using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMeterApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeterData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeterModel = table.Column<double>(type: "float", nullable: false),
                    EquipmentIdentifier = table.Column<double>(type: "float", nullable: false),
                    Timestamp = table.Column<double>(type: "float", nullable: false),
                    ElectricityImportLowTariff_Value = table.Column<double>(type: "float", nullable: false),
                    ElectricityImportLowTariff_Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ElectricityImportHighTariff_Value = table.Column<double>(type: "float", nullable: false),
                    ElectricityImportHighTariff_Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ElectricityExportLowTariff_Value = table.Column<double>(type: "float", nullable: false),
                    ElectricityExportLowTariff_Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ElectricityExportHighTariff_Value = table.Column<double>(type: "float", nullable: false),
                    ElectricityExportHighTariff_Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TariffIndicator = table.Column<double>(type: "float", nullable: false),
                    TotalActivePowerExported_Value = table.Column<double>(type: "float", nullable: false),
                    TotalActivePowerExported_Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalImportedActiveEnergy = table.Column<double>(type: "float", nullable: false),
                    EventLogCount = table.Column<double>(type: "float", nullable: false),
                    CurrentPowerUsage_Value = table.Column<double>(type: "float", nullable: false),
                    CurrentPowerUsage_Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentPowerDelivery_Value = table.Column<double>(type: "float", nullable: false),
                    CurrentPowerDelivery_Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstantaneousActivePowerL1_Value = table.Column<double>(type: "float", nullable: false),
                    InstantaneousActivePowerL1_Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstantaneousActivePowerL2_Value = table.Column<double>(type: "float", nullable: false),
                    InstantaneousActivePowerL2_Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Voltage_Value = table.Column<double>(type: "float", nullable: false),
                    Voltage_Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Current_Value = table.Column<double>(type: "float", nullable: false),
                    Current_Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeterStatus = table.Column<double>(type: "float", nullable: false),
                    MaxContractualPower_Value = table.Column<double>(type: "float", nullable: false),
                    MaxContractualPower_Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxCurrent_Value = table.Column<double>(type: "float", nullable: false),
                    MaxCurrent_Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GasDeviceTypeId = table.Column<double>(type: "float", nullable: false),
                    GasDeviceId = table.Column<double>(type: "float", nullable: false),
                    GasReadStatus = table.Column<double>(type: "float", nullable: false),
                    GasConsumption = table.Column<double>(type: "float", nullable: false),
                    IsLogAnnul = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeterData", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeterData");
        }
    }
}
