namespace TestNamespace;

public partial class PropertyEncapsulation
{
    [PropertyEncapsulation(EPropertyEncapsulation.Public)]
    private int _publicField;
                                                         
    [PropertyEncapsulation(EPropertyEncapsulation.Protected)]
    private int _protectedField;
                                                         
    [PropertyEncapsulation(EPropertyEncapsulation.Private)]
    private int _privateField;
                                                         
    [PropertyEncapsulation(EPropertyEncapsulation.Internal)]
    private int _internalField;
                                                         
    [PropertyEncapsulation(EPropertyEncapsulation.ProtectedInternal)]
    private int _protectedInternalField;
}