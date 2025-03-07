using DigitalRune.Windows.TextEditor;
using DigitalRune.Windows.TextEditor.Completion;
using DigitalRune.Windows.TextEditor.Document;
using DigitalRune.Windows.TextEditor.Formatting;
using DigitalRune.Windows.TextEditor.Highlighting;
using DigitalRune.Windows.TextEditor.Insight;
using DigitalRune.Windows.TextEditor.Markers;
using ExpressionEvaluator_App;
using Furesoft.Core.ExpressionEvaluator;
using Furesoft.Core.ExpressionEvaluator.Library;
using Furesoft.Core.ExpressionEvaluator.Macros;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nardole;

public partial class MainForm : Form
{
    private const string defaultContent = "";

    private Furesoft.Core.ExpressionEvaluator.ExpressionParser _evaluator;
    private EvaluationResult _result;
    private string fileName = String.Empty;
    private Scope initScope;

    public MainForm()
    {
        InitializeComponent();

        ExpressionParser.Init();

        // Show the default text in the editor
        textEditorControl.Document.TextContent = defaultContent;

        // Set the syntax-highlighting for C#
        textEditorControl.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighter("Math");
        // Set the formatting for C#
        textEditorControl.Document.FormattingStrategy = new CSharpFormattingStrategy();

        textEditorControl.DocumentChanged += Document_TextContentChanged;

        Font consolasFont = new Font("Consolas", 15f);
        if (consolasFont.Name == "Consolas")        // Set font if it is available on this machine.
            textEditorControl.Font = consolasFont;

        _evaluator = new ExpressionParser();
        _evaluator.RootScope.Import(typeof(Math));
        _evaluator.RootScope.Import(typeof(Core));
        _evaluator.Import(typeof(Geometry));
        _evaluator.Import(typeof(GeometryTypes));
        _evaluator.Import(typeof(Formulars));

        initScope = Scope.CreateScope();
        initScope.ImportScope(_evaluator.RootScope);

        Macro.OutputChannel.Subscribe(OutputChannel_Receive);
    }

    private void OutputChannel_Receive(object obj)
    {
        if(obj is Image img)
        {
            pictureBox1.Image = img;
            pictureBox1.Invalidate();
        }
    }

    private void CompletionRequest(object sender, CompletionEventArgs e)
    {
        if (textEditorControl.CompletionWindowVisible)
            return;

        // Prevent CompletionRequest to call ShowCompletionWindow if user type a string.
        if (TextHelper.FindStringStart(textEditorControl.Document, textEditorControl.ActiveTextAreaControl.Caret.Offset) != -1 && TextHelper.FindStringStart(textEditorControl.Document, textEditorControl.ActiveTextAreaControl.Caret.Offset) != -1)
            return;

        // e.Key contains the key that the user wants to insert and which triggered
        // the CompletionRequest.
        // e.Key == '\0' means that the user triggered the CompletionRequest by pressing <Ctrl> + <Space>.

        if (e.Key == '\0')
        {
            // The user has requested the completion window by pressing <Ctrl> + <Space>.
            textEditorControl.ShowCompletionWindow(new CodeCompletionDataProvider(_evaluator), e.Key, false);
        }
        else if (char.IsLetter(e.Key) || e.Key == '.')
        {
            // The user is typing normally.
            // -> Show the completion to provide suggestions. Automatically close the window if the
            // word the user is typing does not match the completion data. (Last argument.)
            textEditorControl.ShowCompletionWindow(new CodeCompletionDataProvider(_evaluator), e.Key, true);
        }
    }

    private void Document_TextContentChanged(object sender, DocumentEventArgs e)
    {
        
    }

    private Furesoft.Core.CodeDom.CodeDOM.Annotations.Message GetErrorByPosition(TextLocation postion, LineSegment line)
    {
        if (_result != null)
        {
            foreach (var error in _result.Errors)
            {
                if ((error.ColumnNumber >= postion.X + 1 || error.ColumnNumber <= postion.X + line.Length + 1) && error.LineNumber == postion.Y + 1)
                {
                    return error;
                }
            }
        }

        return null;
    }

    private void InsightRequest(object sender, InsightEventArgs e)
    {
        textEditorControl.ShowInsightWindow(new MethodInsightDataProvider(_evaluator));
    }

    private void ToolTipRequest(object sender, ToolTipRequestEventArgs e)
    {
        if (!e.InDocument || e.ToolTipShown)
            return;

        // Get word under cursor
        TextLocation position = e.LogicalPosition;
        LineSegment line = textEditorControl.Document.GetLineSegment(position.Y);

        var error = GetErrorByPosition(position, line);

        if (error != null)
            e.ShowToolTip(error.Text);
    }

    private void runToolStripMenuItem_Click(object sender, EventArgs e)
    {
        textEditorControl.Document.MarkerStrategy.Clear();

        _evaluator.RootScope = Scope.CreateScope();
        _evaluator.Binder.ArgumentConstraints.Clear();

        _evaluator.RootScope.ImportScope(initScope);

        _result = _evaluator?.Evaluate(textEditorControl.Text);

        if (_result != null)
        {
            richTextBox1.Text = string.Join('\n', _result.Values.Select(_ => Print(_.Get())));
        }

        if (_result != null)
        {
            if (_result.Errors.Any())
            {
                foreach (var error in _result.Errors)
                {
                    var offset = textEditorControl.Document.GetLineSegment(error.LineNumber - 1);

                    Marker marker = new Marker(offset.Offset, error.Parent._AsString.Length, MarkerType.WaveLine);
                    textEditorControl.Document.MarkerStrategy.AddMarker(marker);
                }
            }
        }

        textEditorControl.Refresh();
    }

    private string Print(object v)
    {
        if(v is Matrix<double> m)
        {
            var sb = new StringBuilder();
            sb.Append("[");

            sb.Append(string.Join(", ", m.Enumerate()));

            sb.Append("]");

            return sb.ToString();
        }

        return v.ToString();
    }
}
