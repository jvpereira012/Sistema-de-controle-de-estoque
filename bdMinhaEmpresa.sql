-- drop database bd_MinhaEmpresa_ETIM;

create database bd_MinhaEmpresa_ETIM;

use bd_MinhaEmpresa_ETIM;

create table tb_Login (
	log_usuario varchar(15) primary key,
    log_senha varchar(8) not null,
    log_nome varchar(30) not null,
    log_ult_Atualizacao timestamp not null
);

select * from tb_Login;

CREATE TABLE tb_Produtos (
     CodProd VarChar(5) primary key,
     DescrProd varchar(30) not null,
     QtdEstoq decimal(5,2) not null,
     Locacao varchar(3) not null,
     PrcCusto decimal(6,2) not null,
     PrcVenda decimal(6,2) not null,
     cur_ult_Atualizacao timestamp DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
     );  
     
select * from tb_Produtos;     