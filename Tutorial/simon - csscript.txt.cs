string boilerplate = @"

    using Sept1983Client;

    private static CssInterpreter hostEnvironment;

    private static void Log(string message)
    {{
        hostEnvironment.WriteLine(message);
    }}

    private static void Run(string className)
    {{
        hostEnvironment.LoadScript(className);
    }}

    public static void Evaluate(CssInterpreter callee)
    {{   
        hostEnvironment = callee;                                   
        {0}; // here goes the code that is to be evaluated.  
    }}";

Assembly assembly = CSScript.LoadMethod(string.Format(boilerplate, input));
AsmHelper assemblyHelper = new AsmHelper(assembly);
assemblyHelper.Invoke("*.Evaluate", this);