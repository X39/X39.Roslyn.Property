﻿using System;

namespace X39.SourceGenerators.Property;

/// <summary>
/// Tells the generator to generate properties for the annotated class or field.
/// Use this attribute if other attributes are not desired.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class GeneratePropertiesAttribute : Attribute { }