﻿using System;
using System.Collections.Generic;
using System.Text;
using FrontEnd;


namespace Backend
{
    using SymbolTable = Dictionary<string, Identity>;

    public record Quaternary
    {
        public List<string> Labels { get; init; }
        public string Op { get; set; }
        public string Src1 { get; set; }
        public string Src2 { get; set; }
        public string Dist { get; set; }

        public override string ToString()
        {
            return $"{string.Join(" ,", Labels)} \n {Op}, {Src1}, {Src2}, {Dist}";
        }
    }

    public class Backend
    {
        public List<Quaternary> IrList { get; } = new();
        public Dictionary<string, SymbolTable> Tables { get; }

        private readonly Dictionary<string, string> _typeMap = new()
        {
            {"int", "word"},
            {"short", "half"},
            {"char", "byte"}
        };

        public Backend(string ir, Dictionary<string, SymbolTable> tables)
        {
            Tables = tables;

            var semicolon = 0;
            StringBuilder stringBuilder = new();
            var tmpQuaternary = new Quaternary()
            {
                Labels = new List<string>()
            };

            foreach (var c in ir)
            {
                switch (c)
                {
                    case ' ' or '\n' or '\t' or '\r':
                        break;
                    case ':':
                        tmpQuaternary.Labels.Add(stringBuilder.ToString());
                        stringBuilder.Clear();
                        break;
                    case ';':
                        semicolon++;
                        switch (semicolon)
                        {
                            case 1:
                                tmpQuaternary.Op = stringBuilder.ToString();
                                break;
                            case 2:
                                tmpQuaternary.Src1 = stringBuilder.ToString();
                                break;
                            case 3:
                                tmpQuaternary.Src2 = stringBuilder.ToString();
                                break;
                            case 4:
                                tmpQuaternary.Dist = stringBuilder.ToString();
                                IrList.Add(tmpQuaternary);
                                tmpQuaternary = new Quaternary()
                                {
                                    Labels = new List<string>()
                                };
                                semicolon = 0;
                                break;
                            default:
                                throw new Exception();
                        }

                        stringBuilder.Clear();
                        break;
                    default:
                        stringBuilder.Append(c);
                        break;
                }
            }
        }


        public string DataSegment()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine(".DATA");

            foreach (var quaternary in IrList)
            {
            }

            return "";
        }

        public string Translate()
        {
            foreach (var quaternary in IrList)
            {
                switch (quaternary.Op)
                {
                    case "+":

                        break;
                }
            }

            return "";
        }
    }
}