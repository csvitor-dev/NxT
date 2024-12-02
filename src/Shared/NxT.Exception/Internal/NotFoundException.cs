using NxT.Exception.Base;

namespace NxT.Exception.Internal;

public class NotFoundException(string? message = null) : InternalException(message);
