using NxT.Exception.Base;

namespace NxT.Exception.Internal;

public class IntegrityException(string? message = null) : InternalException(message);
