using NxT.Exception.Base;

namespace NxT.Exception.Internal;

public class DbConcurrencyException(string? message = null) : InternalException(message);