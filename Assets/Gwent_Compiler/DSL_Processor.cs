using System.IO;
using System.Collections.Generic;
using GwentInterpreters;
public class DSLProcessor
{
    public static List<CardOld> LoadCardsFromDSL(string filePath)
    {
        string dslCode = File.ReadAllText(filePath);

        // Parsear el código DSL
        Parser parser = new Parser(new Scanner(dslCode).ScanTokens());
        List<Stmt> statements = parser.Parse();

        // Crear una instancia del intérprete
        Interpreter interpreter = new Interpreter();

        // Interpretar las declaraciones parseadas
        List<CardOld> cards = interpreter.Interpret(statements);

        return cards;
    }
}