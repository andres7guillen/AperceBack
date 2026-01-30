using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementTaskApplication.DTOs;

public sealed record TaskDto(
Guid Id,
string Title,
string Status,
Guid UserId,
string User
);
