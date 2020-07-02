using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BlazorErp.Server.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    VrijednostUAplikaciji = table.Column<int>(nullable: true),
                    Poredak = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    PunoIme = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: false),
                    Data = table.Column<string>(maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "Dokumenti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    Naziv = table.Column<string>(nullable: true),
                    Putanja = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dokumenti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KorisnikTipoviDodatneInformacije",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    Sifra = table.Column<string>(maxLength: 45, nullable: true),
                    Opis = table.Column<string>(maxLength: 200, nullable: true),
                    Naziv = table.Column<string>(maxLength: 200, nullable: true),
                    Poredak = table.Column<int>(nullable: false),
                    Onemogucen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnikTipoviDodatneInformacije", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogKategorije",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    Sifra = table.Column<string>(maxLength: 100, nullable: false),
                    Naziv = table.Column<string>(maxLength: 1000, nullable: false),
                    VrijednostUAplikaciji = table.Column<int>(nullable: true),
                    Poredak = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogKategorije", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogLeveli",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    Sifra = table.Column<string>(maxLength: 100, nullable: false),
                    Naziv = table.Column<string>(maxLength: 1000, nullable: false),
                    VrijednostUAplikaciji = table.Column<int>(nullable: true),
                    Poredak = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogLeveli", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Moduli",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    Sifra = table.Column<string>(maxLength: 45, nullable: true),
                    Naziv = table.Column<string>(maxLength: 200, nullable: true),
                    Opis = table.Column<string>(maxLength: 200, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moduli", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(maxLength: 200, nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Data = table.Column<string>(maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Postavke",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    NaslovSistema = table.Column<string>(maxLength: 64, nullable: false),
                    TrajanjeSesije = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postavke", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PravoGrupe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    Sifra = table.Column<string>(maxLength: 45, nullable: true),
                    Naziv = table.Column<string>(maxLength: 200, nullable: true),
                    Opis = table.Column<string>(maxLength: 200, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PravoGrupe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VrsteLogAkcija",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    Sifra = table.Column<string>(maxLength: 100, nullable: false),
                    Naziv = table.Column<string>(maxLength: 1000, nullable: false),
                    VrijednostUAplikaciji = table.Column<int>(nullable: true),
                    Poredak = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrsteLogAkcija", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PravaUpravljanjaKorisnicima",
                columns: table => new
                {
                    UlogaUpraviteljaId = table.Column<int>(nullable: false),
                    UlogaUpravljanogId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PravaUpravljanjaKorisnicima", x => new { x.UlogaUpraviteljaId, x.UlogaUpravljanogId });
                    table.ForeignKey(
                        name: "FK_PravaUpravljanjaKorisnicima_AspNetRoles_UlogaUpraviteljaId",
                        column: x => x.UlogaUpraviteljaId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PravaUpravljanjaKorisnicima_AspNetRoles_UlogaUpravljanogId",
                        column: x => x.UlogaUpravljanogId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogAkcije",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Level = table.Column<int>(nullable: false),
                    Kategorija = table.Column<int>(nullable: false),
                    Akcija = table.Column<int>(nullable: false),
                    Opis = table.Column<string>(nullable: true),
                    DatumAkcije = table.Column<DateTime>(nullable: false),
                    KorisnikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogAkcije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogAkcije_AspNetUsers_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogEntiteti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EntitetId = table.Column<int>(nullable: false),
                    Entitet = table.Column<int>(nullable: false),
                    Vrijednost = table.Column<string>(nullable: true),
                    DatumAkcije = table.Column<DateTime>(nullable: false),
                    KorisnikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEntiteti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogEntiteti_AspNetUsers_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tokeni",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    VlasnikId = table.Column<int>(nullable: false),
                    DatumKreiranja = table.Column<DateTime>(nullable: false),
                    DatumIsteka = table.Column<DateTime>(nullable: false),
                    PosljednjiIp = table.Column<string>(maxLength: 256, nullable: true),
                    PosljednjiKlijent = table.Column<string>(maxLength: 256, nullable: true),
                    DatumPosljednjeAkcije = table.Column<DateTime>(nullable: false),
                    Tip = table.Column<int>(nullable: false),
                    UlogaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokeni", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tokeni_AspNetRoles_UlogaId",
                        column: x => x.UlogaId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tokeni_AspNetUsers_VlasnikId",
                        column: x => x.VlasnikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UlogaTipoviDodatneInformacije",
                columns: table => new
                {
                    KorisnikTipDodatneInformacijeId = table.Column<int>(nullable: false),
                    UlogaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UlogaTipoviDodatneInformacije", x => new { x.UlogaId, x.KorisnikTipDodatneInformacijeId });
                    table.ForeignKey(
                        name: "FK_UlogaTipoviDodatneInformacije_KorisnikTipoviDodatneInformac~",
                        column: x => x.KorisnikTipDodatneInformacijeId,
                        principalTable: "KorisnikTipoviDodatneInformacije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UlogaTipoviDodatneInformacije_AspNetRoles_UlogaId",
                        column: x => x.UlogaId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PravoObjekti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    Sifra = table.Column<string>(maxLength: 45, nullable: true),
                    Naziv = table.Column<string>(maxLength: 200, nullable: true),
                    PravoGrupaId = table.Column<int>(nullable: false),
                    ModulId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PravoObjekti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PravoObjekti_Moduli_ModulId",
                        column: x => x.ModulId,
                        principalTable: "Moduli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PravoObjekti_PravoGrupe_PravoGrupaId",
                        column: x => x.PravoGrupaId,
                        principalTable: "PravoGrupe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KorisnikUlogaDodatneInformacije",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    KorisnikTipDodatneInformacijeId = table.Column<int>(nullable: false),
                    Vrijednost = table.Column<int>(nullable: false),
                    KorisnikId = table.Column<int>(nullable: false),
                    UlogaId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnikUlogaDodatneInformacije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KorisnikUlogaDodatneInformacije_KorisnikTipoviDodatneInform~",
                        column: x => x.KorisnikTipDodatneInformacijeId,
                        principalTable: "KorisnikTipoviDodatneInformacije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KorisnikUlogaDodatneInformacije_AspNetUserRoles_KorisnikId_~",
                        columns: x => new { x.KorisnikId, x.UlogaId },
                        principalTable: "AspNetUserRoles",
                        principalColumns: new[] { "UserId", "RoleId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PravoAkcije",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    Sifra = table.Column<string>(maxLength: 100, nullable: true),
                    Naziv = table.Column<string>(maxLength: 200, nullable: true),
                    Opis = table.Column<string>(maxLength: 200, nullable: true),
                    PravoObjektId = table.Column<int>(nullable: false),
                    PravoObjekatId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PravoAkcije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PravoAkcije_PravoObjekti_PravoObjekatId",
                        column: x => x.PravoObjekatId,
                        principalTable: "PravoObjekti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PravoAkcijaUloge",
                columns: table => new
                {
                    PravoAkcijaId = table.Column<int>(nullable: false),
                    UlogaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PravoAkcijaUloge", x => new { x.PravoAkcijaId, x.UlogaId });
                    table.ForeignKey(
                        name: "FK_PravoAkcijaUloge_PravoAkcije_PravoAkcijaId",
                        column: x => x.PravoAkcijaId,
                        principalTable: "PravoAkcije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PravoAkcijaUloge_AspNetRoles_UlogaId",
                        column: x => x.UlogaId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                table: "DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikUlogaDodatneInformacije_KorisnikTipDodatneInformaci~",
                table: "KorisnikUlogaDodatneInformacije",
                column: "KorisnikTipDodatneInformacijeId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikUlogaDodatneInformacije_KorisnikId_UlogaId",
                table: "KorisnikUlogaDodatneInformacije",
                columns: new[] { "KorisnikId", "UlogaId" });

            migrationBuilder.CreateIndex(
                name: "IX_LogAkcije_KorisnikId",
                table: "LogAkcije",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntiteti_KorisnikId",
                table: "LogEntiteti",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Moduli_Sifra",
                table: "Moduli",
                column: "Sifra",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PravaUpravljanjaKorisnicima_UlogaUpravljanogId",
                table: "PravaUpravljanjaKorisnicima",
                column: "UlogaUpravljanogId");

            migrationBuilder.CreateIndex(
                name: "IX_PravoAkcijaUloge_UlogaId",
                table: "PravoAkcijaUloge",
                column: "UlogaId");

            migrationBuilder.CreateIndex(
                name: "IX_PravoAkcije_PravoObjekatId",
                table: "PravoAkcije",
                column: "PravoObjekatId");

            migrationBuilder.CreateIndex(
                name: "IX_PravoAkcije_Sifra",
                table: "PravoAkcije",
                column: "Sifra",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PravoGrupe_Sifra",
                table: "PravoGrupe",
                column: "Sifra",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PravoObjekti_ModulId",
                table: "PravoObjekti",
                column: "ModulId");

            migrationBuilder.CreateIndex(
                name: "IX_PravoObjekti_PravoGrupaId",
                table: "PravoObjekti",
                column: "PravoGrupaId");

            migrationBuilder.CreateIndex(
                name: "IX_PravoObjekti_Sifra",
                table: "PravoObjekti",
                column: "Sifra",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tokeni_UlogaId",
                table: "Tokeni",
                column: "UlogaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokeni_VlasnikId",
                table: "Tokeni",
                column: "VlasnikId");

            migrationBuilder.CreateIndex(
                name: "IX_UlogaTipoviDodatneInformacije_KorisnikTipDodatneInformacije~",
                table: "UlogaTipoviDodatneInformacije",
                column: "KorisnikTipDodatneInformacijeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "Dokumenti");

            migrationBuilder.DropTable(
                name: "KorisnikUlogaDodatneInformacije");

            migrationBuilder.DropTable(
                name: "LogAkcije");

            migrationBuilder.DropTable(
                name: "LogEntiteti");

            migrationBuilder.DropTable(
                name: "LogKategorije");

            migrationBuilder.DropTable(
                name: "LogLeveli");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "Postavke");

            migrationBuilder.DropTable(
                name: "PravaUpravljanjaKorisnicima");

            migrationBuilder.DropTable(
                name: "PravoAkcijaUloge");

            migrationBuilder.DropTable(
                name: "Tokeni");

            migrationBuilder.DropTable(
                name: "UlogaTipoviDodatneInformacije");

            migrationBuilder.DropTable(
                name: "VrsteLogAkcija");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "PravoAkcije");

            migrationBuilder.DropTable(
                name: "KorisnikTipoviDodatneInformacije");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PravoObjekti");

            migrationBuilder.DropTable(
                name: "Moduli");

            migrationBuilder.DropTable(
                name: "PravoGrupe");
        }
    }
}
