﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleCRM.DAL.Migrations
{
    public partial class AnotherMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Tasks_TaskEntityId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "TaskEntityId",
                table: "Tasks",
                newName: "ParentTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_TaskEntityId",
                table: "Tasks",
                newName: "IX_Tasks_ParentTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Tasks_ParentTaskId",
                table: "Tasks",
                column: "ParentTaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Tasks_ParentTaskId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "ParentTaskId",
                table: "Tasks",
                newName: "TaskEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_ParentTaskId",
                table: "Tasks",
                newName: "IX_Tasks_TaskEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Tasks_TaskEntityId",
                table: "Tasks",
                column: "TaskEntityId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
