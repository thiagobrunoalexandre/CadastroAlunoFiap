var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


builder.Services.AddHttpClient<AlunoService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7131/"); 
});
builder.Services.AddHttpClient<TurmaService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7131/"); 
});
builder.Services.AddHttpClient<RelacionamentoService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7131/"); 
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
