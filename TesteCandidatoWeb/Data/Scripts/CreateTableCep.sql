USE [CEP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CEP] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Cep]         CHAR (8)       NULL,
    [Logradouro]  NVARCHAR (500) NULL,
    [Complemento] NVARCHAR (500) NULL,
    [Bairro]      NVARCHAR (500) NULL,
    [Localidade]  NVARCHAR (500) NULL,
    [Uf]          CHAR (2)       NULL,
    [Ibge]        CHAR (7)       NULL,
    [Gia]         CHAR (4)       NULL,
    [Ddd]         CHAR (2)       NULL,
    [Siafi]       CHAR (4)       NULL,
);