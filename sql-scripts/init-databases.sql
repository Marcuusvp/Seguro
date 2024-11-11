CREATE DATABASE [SEGURODB];
GO

CREATE DATABASE [HistoricoDb];
GO

USE SEGURODB
GO

DELETE FROM RegrasCalculo;

INSERT INTO RegrasCalculo (Id, Nome, Tipo, ConteudoJson, Ativa)
VALUES (NEWID(), 'Regra de Pontuação por Idade', 'PontuacaoIdade', '[
    { "IdadeMinima": 18, "IdadeMaxima": 25, "Pontuacao": 15 },
    { "IdadeMinima": 26, "IdadeMaxima": 40, "Pontuacao": 5 },
    { "IdadeMinima": 41, "IdadeMaxima": 60, "Pontuacao": 3 },
    { "IdadeMinima": 61, "IdadeMaxima": 120, "Pontuacao": 10 }
]
', 1);

INSERT INTO RegrasCalculo (Id, Nome, Tipo, ConteudoJson, Ativa)
VALUES (NEWID(), 'Regra de Pontuação por Acidentes', 'PontuacaoAcidentes', '[
    { "AcidentesMinimos": 0, "AcidentesMaximos": 0, "Pontuacao": 0 },
    { "AcidentesMinimos": 1, "AcidentesMaximos": 1, "Pontuacao": 10 },
    { "AcidentesMinimos": 2, "AcidentesMaximos": 2, "Pontuacao": 20 },
    { "AcidentesMinimos": 3, "AcidentesMaximos": 1000, "Pontuacao": 30 }
]
', 1);

INSERT INTO RegrasCalculo (Id, Nome, Tipo, ConteudoJson, Ativa)
VALUES (NEWID(), 'Regra de Pontuação por Localidade', 'PontuacaoLocalidade', '[
    { "NivelRiscoLocalidade": "Baixo", "Pontuacao": 5 },
    { "NivelRiscoLocalidade": "Médio", "Pontuacao": 10 },
    { "NivelRiscoLocalidade": "Alto", "Pontuacao": 20 }
]
', 1);

INSERT INTO RegrasCalculo (Id, Nome, Tipo, ConteudoJson, Ativa)
VALUES (NEWID(), 'Regra de Classificação por nivel de Risco', 'ClassificacaoRisco', '[
    { "PontuacaoMinima": 0, "PontuacaoMaxima": 10, "NivelRisco": 1 },
    { "PontuacaoMinima": 11, "PontuacaoMaxima": 25, "NivelRisco": 2 },
    { "PontuacaoMinima": 26, "PontuacaoMaxima": 40, "NivelRisco": 3 },
    { "PontuacaoMinima": 41, "PontuacaoMaxima": 55, "NivelRisco": 4 },
    { "PontuacaoMinima": 56, "PontuacaoMaxima": 1000, "NivelRisco": 5 }
]
', 1);

INSERT INTO RegrasCalculo (Id, Nome, Tipo, ConteudoJson, Ativa)
VALUES (NEWID(), 'Custo por coberturas', 'CustoCoberturas', '[
    { "Cobertura": "RouboFurto", "PercentualCustoBase": 3.0, "CustoFixo": 0 },
    { "Cobertura": "Colisao", "PercentualCustoBase": 4.0, "CustoFixo": 0 },
    { "Cobertura": "Terceiros", "PercentualCustoBase": 1.5, "CustoFixo": 0 },
    { "Cobertura": "ProtecaoResidencial", "PercentualCustoBase": 0, "CustoFixo": 100 }
]
', 1);

INSERT INTO RegrasCalculo (Id, Nome, Tipo, ConteudoJson, Ativa)
VALUES (NEWID(), 'Ajuste por nivel de risco', 'AjusteNivelRisco', '[
    { "NivelRisco": 1, "PercentualAjuste": 0 },
    { "NivelRisco": 2, "PercentualAjuste": 5 },
    { "NivelRisco": 3, "PercentualAjuste": 10 },
    { "NivelRisco": 4, "PercentualAjuste": 20 },
    { "NivelRisco": 5, "PercentualAjuste": 30 }
]
', 1);

DELETE FROM RiscoPorLocalidade;

-- Inserir dados
INSERT INTO RiscoPorLocalidade (Id, UF, Cidade, Bairro, NivelRisco)
VALUES
-- Região Norte
(NEWID(), 'AC', 'Rio Branco', 'Centro', 'Médio'),
(NEWID(), 'AC', 'Rio Branco', 'Bosque', 'Baixo'),
(NEWID(), 'AC', 'Rio Branco', 'Estação Experimental', 'Médio'),
(NEWID(), 'AC', 'Rio Branco', 'Jardim Primavera', 'Alto'),
(NEWID(), 'AC', 'Rio Branco', 'Floresta', 'Médio'),
(NEWID(), 'AC', 'Rio Branco', 'Vila Nova', 'Baixo'),
(NEWID(), 'AC', 'Rio Branco', 'Tangará', 'Médio'),
(NEWID(), 'AC', 'Rio Branco', 'Novo Horizonte', 'Alto'),
(NEWID(), 'AC', 'Rio Branco', 'Esperança', 'Baixo'),
(NEWID(), 'AC', 'Rio Branco', 'Aeroporto Velho', 'Médio'),

-- Região Nordeste
(NEWID(), 'BA', 'Salvador', 'Barra', 'Médio'),
(NEWID(), 'BA', 'Salvador', 'Ondina', 'Baixo'),
(NEWID(), 'BA', 'Salvador', 'Pituba', 'Médio'),
(NEWID(), 'BA', 'Salvador', 'Itapuã', 'Alto'),
(NEWID(), 'BA', 'Salvador', 'Rio Vermelho', 'Médio'),
(NEWID(), 'BA', 'Salvador', 'Graça', 'Baixo'),
(NEWID(), 'BA', 'Salvador', 'Amaralina', 'Médio'),
(NEWID(), 'BA', 'Salvador', 'Brotas', 'Alto'),
(NEWID(), 'BA', 'Salvador', 'São Cristóvão', 'Baixo'),
(NEWID(), 'BA', 'Salvador', 'Cabula', 'Médio'),

-- Região Centro-Oeste
(NEWID(), 'DF', 'Brasília', 'Asa Sul', 'Baixo'),
(NEWID(), 'DF', 'Brasília', 'Asa Norte', 'Baixo'),
(NEWID(), 'DF', 'Brasília', 'Taguatinga', 'Médio'),
(NEWID(), 'DF', 'Brasília', 'Ceilândia', 'Alto'),
(NEWID(), 'DF', 'Brasília', 'Samambaia', 'Médio'),
(NEWID(), 'DF', 'Brasília', 'Guará', 'Baixo'),
(NEWID(), 'DF', 'Brasília', 'Planaltina', 'Médio'),
(NEWID(), 'DF', 'Brasília', 'Recanto das Emas', 'Alto'),
(NEWID(), 'DF', 'Brasília', 'Sobradinho', 'Baixo'),
(NEWID(), 'DF', 'Brasília', 'Gama', 'Médio'),

-- Região Sudeste
(NEWID(), 'SP', 'São Paulo', 'Centro', 'Médio'),
(NEWID(), 'SP', 'São Paulo', 'Moema', 'Baixo'),
(NEWID(), 'SP', 'São Paulo', 'Itaquera', 'Alto'),
(NEWID(), 'SP', 'São Paulo', 'Pinheiros', 'Baixo'),
(NEWID(), 'SP', 'São Paulo', 'Santana', 'Médio'),
(NEWID(), 'SP', 'São Paulo', 'Vila Mariana', 'Baixo'),
(NEWID(), 'SP', 'São Paulo', 'Morumbi', 'Médio'),
(NEWID(), 'SP', 'São Paulo', 'Capão Redondo', 'Alto'),
(NEWID(), 'SP', 'São Paulo', 'Tatuapé', 'Médio'),
(NEWID(), 'SP', 'São Paulo', 'Lapa', 'Baixo'),

-- Região Sul
(NEWID(), 'RS', 'Porto Alegre', 'Centro Histórico', 'Médio'),
(NEWID(), 'RS', 'Porto Alegre', 'Moinhos de Vento', 'Baixo'),
(NEWID(), 'RS', 'Porto Alegre', 'Cidade Baixa', 'Médio'),
(NEWID(), 'RS', 'Porto Alegre', 'Restinga', 'Alto'),
(NEWID(), 'RS', 'Porto Alegre', 'Cavalhada', 'Médio'),
(NEWID(), 'RS', 'Porto Alegre', 'Menino Deus', 'Baixo'),
(NEWID(), 'RS', 'Porto Alegre', 'Partenon', 'Médio'),
(NEWID(), 'RS', 'Porto Alegre', 'Lomba do Pinheiro', 'Alto'),
(NEWID(), 'RS', 'Porto Alegre', 'Petrópolis', 'Baixo'),
(NEWID(), 'RS', 'Porto Alegre', 'Sarandi', 'Médio'),

-- Região Nordeste (Outro exemplo)
(NEWID(), 'PE', 'Recife', 'Boa Viagem', 'Baixo'),
(NEWID(), 'PE', 'Recife', 'Casa Amarela', 'Médio'),
(NEWID(), 'PE', 'Recife', 'Ibura', 'Alto'),
(NEWID(), 'PE', 'Recife', 'Pina', 'Médio'),
(NEWID(), 'PE', 'Recife', 'Tamarineira', 'Baixo'),
(NEWID(), 'PE', 'Recife', 'Cordeiro', 'Médio'),
(NEWID(), 'PE', 'Recife', 'Várzea', 'Alto'),
(NEWID(), 'PE', 'Recife', 'Encruzilhada', 'Médio'),
(NEWID(), 'PE', 'Recife', 'Graças', 'Baixo'),
(NEWID(), 'PE', 'Recife', 'Jaqueira', 'Médio')
