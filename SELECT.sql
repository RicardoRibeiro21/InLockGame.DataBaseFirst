------------------------------SELECT----------------------------
SELECT * FROM USUARIOS

SELECT * FROM ESTUDIOS

SELECT * FROM JOGOS
go
--Relacionando a tabela jogos com a de estudio
CREATE VIEW JOGOS_ESTUDIO_JOIN
	AS SELECT J.JogoId, J.NomeJogo, J.Descricao, J.DataLancamento, J.Valor, E.NomeEstudio AS Nome_Estudio, E.EstudioId AS EstudioId FROM JOGOS AS J INNER JOIN ESTUDIOS E ON 
	J.EstudioId = E.EstudioId;	
go

SELECT * FROM JOGOS_ESTUDIO_JOIN 
--Relacionando a tabela jogos com a de estudio mesmo que não há referência de jogos
CREATE VIEW JOGOS_ESTUDIOS
AS SELECT J.JogoId, J.NomeJogo, J.Descricao, J.DataLancamento, E.NomeEstudio FROM JOGOS AS J RIGHT JOIN ESTUDIOS E ON 
	J.EstudioId = E.EstudioId;
go

SELECT * FROM JOGOS_ESTUDIOS

--Buscando o Usuário por email e senha
SELECT * FROM USUARIOS WHERE USUARIOS.Email = 'cliente@cliente.com' and USUARIOS.Senha = 'cliente'

--Buscando um Jogo pelo Id
SELECT * FROM JOGOS WHERE JOGOS.JogoId = 9

--Buscando um estúdio pelo Id
SELECT * FROM ESTUDIOS WHERE ESTUDIOS.EstudioId = 1
	