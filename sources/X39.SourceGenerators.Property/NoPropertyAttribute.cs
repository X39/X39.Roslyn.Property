﻿using System;

namespace X39.SourceGenerators.Property;

/// <summary>
/// Instructs the generator to not generate properties for the annotated field.
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class NoPropertyAttribute : Attribute { }