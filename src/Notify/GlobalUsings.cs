// built-in
global using System.Text.Json;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Options;
global using System.Text;
 global using System.Net;
 global using System.Net.Mail;



// third parties
global using MediatR;
global using MongoDB.EntityFrameworkCore;
global using MongoDB.EntityFrameworkCore.Extensions;
global using MongoDB.Bson;
global using MassTransit;
global using Carter;

// solution
global using Notify;
global using Notify.Common;
global using Notify.Common.Abstractions;
global using Notify.Common.Inbox;
global using Notify.Features.Sms.Messages;
global using Notify.Features.Sms;
global using Notify.Features.Sms.Providers;
global using Notify.Features.Email.Messages;
global using Notify.Features.Email;
global using Notify.Features.Sms.Services;
global using Notify.Features.Email.Services;
global using Notify.Features.Email.Dtos;