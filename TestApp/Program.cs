﻿using System;
using System.IO;

namespace TestApp
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // For demonstartion purpose, our cow database is saved into a temp file
            var dbFile = Path.Combine(Environment.CurrentDirectory, "cowdb.data");

            while (true)
            {
                var cmd = Console.ReadLine();

                if (cmd == "all")
                {
                    using var db = new CowDatabase(dbFile);
                    foreach (var cow in db.FindAll())
                    {
                        Console.WriteLine(cow);
                    }
                }
                else if (cmd == "add")
                {
                    var name = Console.ReadLine();
                    using var db = new CowDatabase(dbFile);
                    db.Insert(new CowModel { Name = name, Id = Guid.NewGuid(), Breed = name, DnaData = new byte[] { 20, 40, 36, 21 } });
                }
                else if (cmd == "init")
                {
                    using (var db = new CowDatabase(dbFile))
                    {
                        db.Insert(new CowModel
                        {
                            Id = Guid.Parse("8872d8ba-e470-440d-aa9b-071822e8053f"),
                            Breed = "Golden Retriever",
                            Name = "GR1",
                            Age = 12,
                            DnaData = new byte[] {
                            0x3A, 0xAB, 0x7D, 0xBF, 0xDE, 0xF0, 0xDF, 0xE0, 0x45, 0xED, 0xC9, 0x01, 0xB0, 0x87, 0x74, 0xFE, 0xD3, 0x00, 0x62, 0xDB, 0x7B, 0x28, 0x7A, 0xA2, 0xA2, 0x13, 0xAB, 0xB9, 0x98, 0x07, 0x54, 0x88, 0xF0, 0x2A, 0xED, 0x92, 0x72, 0x50, 0x78, 0x90, 0x65, 0xC4, 0x2B, 0x42, 0x86, 0x7E, 0x16, 0x63, 0x39, 0x87, 0x7F, 0x3C, 0x9F, 0xB7, 0x5A, 0x20, 0xEF, 0x0F, 0x73, 0x64, 0xC0, 0x27, 0x6B, 0x01, 0xC2, 0xDD, 0xF2, 0x86, 0x10, 0xB3, 0xFC, 0xCD, 0x46, 0x02, 0x57, 0xC0, 0x34, 0xA7, 0xDC, 0x6D,
                            0xEA, 0xB2, 0xC5, 0x1B, 0x6A, 0xE7, 0x72, 0x40, 0x34, 0x30, 0xBE, 0xC5, 0x6D, 0xAA, 0x81, 0x95, 0xAE, 0x45, 0x19, 0x1E, 0x44, 0xBA, 0x94, 0x80, 0x0A, 0x38, 0x85, 0x1F, 0xD6, 0xCF, 0xB6, 0x35, 0x18, 0xD8, 0xEF, 0xEA, 0x34, 0xB6, 0x31, 0xC1, 0x49, 0xAD, 0xE0, 0xD4, 0xC4, 0x7B, 0xC1, 0x3E, 0x15, 0x90, 0x7F, 0x20, 0xB9, 0x53, 0x9B, 0xCC, 0x9B, 0xA1, 0xB5, 0x60, 0xB8, 0xC9, 0x73, 0xD1, 0x1D, 0x4D, 0x7B, 0x94, 0x79, 0x15, 0xBA, 0xA1, 0x8A, 0x11, 0x72, 0x2C, 0xF8, 0x5E, 0x38, 0x88,
                            0xB3, 0x2C, 0x6E, 0x4E, 0x25, 0xA2, 0x30, 0x51, 0xEB, 0x1C, 0x2C, 0xAE, 0xA8, 0xEE, 0x6A, 0x1E, 0x09, 0x97, 0x8D, 0xAA, 0x63, 0xE1, 0x80, 0x9E, 0x34, 0x08, 0x7B, 0x6F, 0x93, 0x2E, 0x05, 0x87, 0x4B, 0x19, 0x6E, 0x80, 0x07, 0x6E, 0xD8, 0x6A, 0xFF, 0xB1, 0xA9, 0x74, 0x3A, 0xD4, 0xBC, 0x90, 0xA7, 0xDC, 0xFF, 0x74, 0xBF, 0x70, 0x57, 0x21, 0xC7, 0xF0, 0x96, 0xCA, 0x4C, 0x6C, 0x04, 0x44, 0xD5, 0x45, 0xB9, 0xBF, 0x76, 0x6B, 0x16, 0xFB, 0x7C, 0x83, 0xD9, 0x64, 0xB2, 0x82, 0x8A, 0x37,
                            0xA3, 0x3F, 0x3D, 0x53, 0xCF, 0x82, 0x01, 0x9D, 0xDA, 0x44, 0xC1, 0x64, 0xEF, 0xA8, 0x08, 0x6C, 0x2F, 0x0D, 0x3F, 0x8A, 0x65, 0x7D, 0x03, 0x49, 0x9B, 0x4E, 0x5F, 0xB9, 0xCF, 0x03, 0x2D, 0x23, 0x8B, 0xF0, 0x1B, 0xAD, 0x2A, 0xD7, 0x99, 0x3B, 0xB0, 0xE5, 0x38, 0x63, 0x36, 0x14, 0xF4, 0x79, 0x23, 0x44, 0x33, 0x8E, 0x17, 0xCB, 0x24, 0xF3, 0xB5, 0x82, 0xEC, 0xF0, 0x4C, 0xDA, 0x96, 0xED, 0x23, 0x46, 0x5F, 0x77, 0x70, 0x4C, 0xCB, 0x0F, 0x6E, 0x89, 0xB4, 0xB5, 0x34, 0xF7, 0x6D, 0x4A,
                            0x8E, 0x9E, 0xF8, 0xDB, 0xE1, 0xBD, 0x83, 0x39, 0xCD, 0x4B, 0xD4, 0x1D, 0x8E, 0x31, 0xFF, 0x19, 0xB9, 0x3D, 0x22, 0x0D, 0x0C, 0xBA, 0x9D, 0xDF, 0x05, 0xB9, 0x98, 0xD4, 0xDB, 0x51, 0x5C, 0xCE, 0xF1, 0x1E, 0xF6, 0x79, 0xD6, 0xB0, 0x96, 0x4B, 0x03, 0x09, 0x48, 0xF3, 0xCF, 0x06, 0xCE, 0x83, 0x0C, 0xB7, 0xBF, 0x0A, 0xE1, 0xB3, 0xDC, 0xA6, 0x7A, 0x01, 0xC2, 0x97, 0x2E, 0x2F, 0x50, 0xDE, 0xD7, 0x0C, 0xBC, 0xF1, 0x36, 0xB3, 0xCB, 0x50, 0x59, 0xF2, 0xD6, 0x7E, 0x33, 0x5C, 0xC1, 0x4E,
                            0x4D, 0x55, 0xBD, 0xBA, 0xAF, 0xE5, 0x75, 0xB6, 0x92, 0xAB, 0x1B, 0x06, 0x02, 0x98, 0x73, 0x42, 0x1D, 0xFE, 0x5F, 0x47, 0xE4, 0x7E, 0x81, 0x7D, 0x7E, 0x3A, 0x69, 0xC9, 0xCF, 0x59, 0x42, 0x36, 0xAC, 0x49, 0x6C, 0x09, 0xD3, 0xDC, 0x32, 0xEE, 0xBB, 0x08, 0xF9, 0xB8, 0x06, 0x26, 0xAA, 0x2B, 0x11, 0x3B, 0xDD, 0x7A, 0xD9, 0xD3, 0xB0, 0x69, 0x06, 0xE8, 0x2A, 0x60, 0x0B, 0x9F, 0xA9, 0xF2, 0x01, 0xA2, 0xE2, 0x1D, 0x46, 0xA6, 0xC8, 0xCF, 0x08, 0x64, 0xC9, 0x96, 0xA3, 0xA2, 0x37, 0x04,
                            0xAA, 0x2A, 0x64, 0xBD, 0x50, 0x3C, 0xF0, 0x48, 0xEA, 0xF2, 0x3A, 0x67, 0x83, 0x7B, 0x7F, 0x9D, 0x56, 0xB2, 0x62, 0x9C, 0xD8, 0xEB, 0x6D, 0x79, 0xF2, 0x39, 0x7E, 0x0F, 0x3A, 0x22, 0xCF, 0x66, 0x66, 0x69, 0x51, 0x2C, 0x12, 0x4B, 0xCA, 0x1A, 0xCE, 0xC5, 0xB0, 0x57, 0x5B, 0xEB, 0x06, 0x0D, 0x28, 0x4F, 0x7D, 0x4D, 0x06, 0x85, 0x95, 0x39, 0x97, 0x6D, 0x30, 0xA6, 0x0E, 0x39, 0x36, 0xCA, 0x27, 0x93, 0x61, 0x3E, 0xAE, 0x55, 0x70, 0xE0, 0x85, 0x18, 0x03, 0x91, 0xED, 0xF8, 0x9A, 0xA9,
                            0xAD, 0x31, 0xC0, 0x28, 0x37, 0x25, 0xB5, 0xF0, 0xF3, 0x1E, 0x53, 0xAF, 0xE7, 0x58, 0x1B, 0x5F, 0x4B, 0xAA, 0xDD, 0x38, 0x28, 0x49, 0x04, 0x58, 0xEF, 0x86, 0x9B, 0x36, 0x23, 0x34, 0xAA, 0x78, 0x23, 0x0F, 0xA4, 0x22, 0x09, 0xBC, 0xF1, 0xFA, 0x2C, 0x92, 0x2E, 0x58, 0x84, 0x79, 0xAE, 0xD7, 0x1C, 0x6C, 0x35, 0xE9, 0xC6, 0x50, 0x07, 0x71, 0x49, 0x2E, 0x04, 0x63, 0xD8, 0x75, 0xD9, 0x81, 0xB3, 0xB2, 0xA2, 0x6C, 0xEF, 0x5B, 0x94, 0x81, 0xD5, 0x41, 0xCE, 0x7C, 0x53, 0xEB, 0xE8, 0xB3
                        }
                        });

                        db.Insert(new CowModel
                        {
                            Id = Guid.Parse("59ee9033-4ec5-40e0-91a7-6c9ecb6e0465"),
                            Breed = "Labrador",
                            Name = "L1",
                            Age = 18,
                            DnaData = new byte[] {
                            0x73, 0xF5, 0x93, 0xB1, 0xAC, 0x47, 0x0F, 0xED, 0x9B, 0x26, 0x08, 0x6A, 0x4F, 0x8A, 0xA1, 0x1A, 0x75, 0xBB, 0x74, 0x7F, 0x4E, 0x5F, 0x66, 0x51, 0x23, 0xFF, 0x95, 0xFF, 0xD4, 0x0A, 0xA0, 0x78, 0xA9, 0xFA, 0x32, 0x55, 0x33, 0x12, 0x27, 0x4D, 0x96, 0x86, 0x21, 0x01, 0x6C, 0xE2, 0x3D, 0x44, 0x7D, 0x4C, 0x6A, 0xD0, 0xD9, 0xC6, 0x4D, 0xC5, 0x03, 0x04, 0x80, 0x58, 0x8E, 0x37, 0x33, 0x26, 0xED, 0xA5, 0x28, 0x7E, 0x87, 0x98, 0x5A, 0xD9, 0xF9, 0x20, 0xCF, 0xB7, 0x7B, 0x38, 0x76, 0xCA,
                            0x75, 0x77, 0x27, 0x38, 0xC1, 0xC0, 0x6D, 0x2E, 0x0A, 0x7A, 0x11, 0xAF, 0x13, 0xD6, 0x4E, 0x71, 0xBE, 0x10, 0x25, 0x78, 0x59, 0x4A, 0x3A, 0x0A, 0x84, 0x7C, 0xF2, 0x95, 0xA8, 0x5E, 0x9D, 0x3D, 0xAA, 0x58, 0xB2, 0xBA, 0x3E, 0x57, 0xDF, 0xB6, 0x33, 0xDF, 0x6B, 0x28, 0x5B, 0x5A, 0x08, 0xF8, 0x56, 0x08, 0x48, 0xE6, 0x8D, 0x80, 0xD1, 0x8B, 0xCE, 0xA5, 0xF4, 0x7F, 0x8C, 0x20, 0xC1, 0xAA, 0xFC, 0x26, 0xF3, 0x1B, 0x56, 0x69, 0x22, 0xB4, 0x3A, 0xE1, 0xE9, 0x81, 0xF1, 0x07, 0x5E, 0xA7,
                            0x2E, 0x00, 0xF1, 0xB9, 0xDF, 0x09, 0xDD, 0xD6, 0x1D, 0xC1, 0x39, 0xC6, 0x40, 0x7E, 0x29, 0x57, 0xCF, 0x63, 0x1B, 0x52, 0xF4, 0x4F, 0x8F, 0x31, 0x3A, 0x68, 0x18, 0x48, 0x29, 0xBE, 0x9C, 0x63, 0x99, 0xCE, 0x79, 0x46, 0x8A, 0x2E, 0x5C, 0xE5, 0xD6, 0xA8, 0x3D, 0x54, 0xBD, 0x17, 0xF6, 0x82, 0xE1, 0x5E, 0x24, 0xAA, 0x50, 0xF6, 0x55, 0x35, 0xB6, 0x03, 0x2D, 0xFB, 0x38, 0x39, 0x7C, 0x61, 0x20, 0xF4, 0x7F, 0x28, 0xB0, 0x23, 0xDF, 0xE5, 0xAC, 0xC8, 0x64, 0x00, 0x19, 0xBA, 0x11, 0x29,
                            0x9E, 0x35, 0x5F, 0xEC, 0x48, 0x72, 0xBC, 0xE3, 0x9E, 0xEB, 0x0E, 0xBD, 0x0F, 0x3D, 0x47, 0x7A, 0xD9, 0x9F, 0xB8, 0xFB, 0xE0, 0x4D, 0xCD, 0x54, 0xB7, 0x13, 0x27, 0x8B, 0x36, 0x56, 0xC0, 0x8D, 0x1D, 0x52, 0x3F, 0x80, 0xE3, 0xC0, 0xAD, 0x02, 0x9E, 0x1C, 0x53, 0x75, 0x0E, 0xC5, 0x38, 0xFD, 0x87, 0xCF, 0x82, 0x1C, 0xF0, 0x4F, 0xA8, 0x15, 0x53, 0xDD, 0xFB, 0x7D, 0x6F, 0x24, 0x32, 0xD6, 0x4F, 0x83, 0x01, 0x96, 0xD4, 0xD4, 0xF8, 0xA7, 0xD6, 0x0F, 0xB1, 0xDC, 0xFF, 0x99, 0x0D, 0x9D,
                            0x3E, 0xBB, 0x0D, 0x53, 0xA6, 0x0A, 0xD2, 0xEE, 0x4A, 0x6C, 0x1D, 0xB2, 0x1B, 0x31, 0x80, 0x5F, 0xB0, 0x10, 0xDF, 0x7F, 0x1C, 0x54, 0x13, 0xFD, 0x1A, 0x46, 0x6C, 0xB6, 0x38, 0xA5, 0xA3, 0xBA, 0x00, 0xEC, 0x1D, 0x15, 0x37, 0xB5, 0x76, 0x1D, 0xB6, 0xFD, 0xBF, 0x9F, 0x5A, 0xD4, 0x42, 0x5D, 0x1B, 0x40, 0x42, 0xB4, 0x9E, 0x3E, 0x05, 0xED, 0x6C, 0x94, 0x3D, 0xE1, 0x6F, 0x76, 0xFC, 0x00, 0x2E, 0x4B, 0x10, 0x99, 0xAB, 0x3C, 0xBA, 0xEF, 0xB0, 0xD8, 0x90, 0x93, 0xD3, 0xA4, 0xA5, 0x4D,
                            0xE2, 0xC2, 0x13, 0xB5, 0x3F, 0xA8, 0xA1, 0x5B, 0x17, 0xAE, 0xE8, 0x71, 0x40, 0xEE, 0x64, 0xE4, 0x65, 0xE9, 0xEB, 0xE8, 0x48, 0x59, 0x61, 0x9B, 0xDE, 0x33, 0xA6, 0xF5, 0xBA, 0xE0, 0x4D, 0xFE, 0x05, 0x14, 0x3C, 0xD5, 0x92, 0x96, 0x2E, 0xB3, 0x72, 0x08, 0x0B, 0x90, 0x68, 0xE3, 0xDD, 0x37, 0x86, 0x80, 0x05, 0x81, 0x0D, 0xA8, 0xC9, 0x3C, 0xFB, 0x91, 0xD6, 0xE5, 0xB0, 0xDA, 0xEA, 0x8B, 0xE2, 0xDC, 0x5A, 0x22, 0xAE, 0x72, 0x11, 0x4D, 0x51, 0x32, 0x42, 0xA2, 0x83, 0x4F, 0x7D, 0x36,
                            0x5E, 0xA9, 0xB7, 0x74, 0x87, 0x7E, 0xFB, 0x4A, 0xC0, 0x4D, 0x65, 0x7C, 0x8B, 0x32, 0x63, 0xAC, 0xB0, 0xD2, 0x6A, 0x1E, 0x9F, 0xAD, 0xAB, 0x3D, 0xEA, 0x27, 0x96, 0x0F, 0x7E, 0x10, 0x35, 0xE4, 0xCA, 0x3E, 0xD1, 0x42, 0xE6, 0x36, 0xB9, 0x1C, 0x76, 0x3C, 0x13, 0x35, 0xFD, 0xB8, 0x80, 0xA4, 0x15, 0x00, 0x84, 0xFD, 0x60, 0x82, 0x2A, 0x3D, 0xD5, 0x8C, 0x12, 0x39, 0x7E, 0xE0, 0x5B, 0x9D, 0xBE, 0x56, 0x91, 0x33, 0x76, 0x60, 0xA0, 0x25, 0xAE, 0xA3, 0x8D, 0x3E, 0x27, 0x8A, 0x6F, 0x7A,
                            0x7B, 0x63, 0x4D, 0x4B, 0x15, 0x5B, 0xC2, 0xE6, 0xB8, 0x67, 0x05, 0x5D, 0xB6, 0x41, 0x89, 0xC8, 0xEC, 0x9F, 0xAE, 0x0C, 0xAF, 0x12, 0x87, 0x2F, 0x82, 0xED, 0x90, 0x91, 0x59, 0x7E, 0x92, 0x37, 0x8D, 0xC0, 0x9F, 0xCD, 0x79, 0xD1, 0x33, 0x54, 0x34, 0x86, 0xBF, 0xEE, 0x27, 0x91, 0x62, 0x37, 0xBA, 0x9A, 0xB2, 0x59, 0x60, 0x50, 0x27, 0xD2, 0x33, 0xA4, 0xAD, 0xC9, 0xEE, 0xD2, 0x6B, 0x65, 0x56, 0x59, 0xAA, 0x80, 0xFF, 0xFA, 0xAE, 0xBB, 0xA7, 0xD2, 0x8F, 0xDE, 0xDF, 0xBC, 0x53, 0x3B
                        }
                        });
                    }
                }
            }
        }
    }
}