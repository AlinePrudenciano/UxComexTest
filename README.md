# UxComexTest
Esse projeto foi criado para etapa de teste no processo seletivo da UxComex.
Criado as tabelas no SQL Server e criado um database project para  reprodução do avaliador.
Criado uma api para facilitar os testes do backend antes de fazer o front, a api é um bônus do meu teste. 


## Especificações

- Projeto desenvolvido em .net 6.0.
- Banco de Dados SQL Server.
- A ORM utilizada foi Dapper.
- Backend respeitando SOLID e DDD.
- Arquitetura MVC.

## Passo a Passo API

Para visualizar a api, favor usar http://localhost:5001/swagger

* Criar um usuário (POST /user)
<br> Exemplo:
```	
  {
	  "name": "usuario1",
	  "cpf": "99999999999",
	  "phone": "11999999999"
	}
  ```
  obs: o id é identity.


* Cradastrar endereço de usuário (POST /address)
<br> Exemplo utilizando o UserId = 1
```
   {
 	 "userId": 1,
  	 "adressname": "endereço1",
  	 "cep": "09570200",
	 "city": "sao caetano",
	 "state": "sao paulo"
	}
  ```

------------------------------------------

## O que pode ser aprimorado:
- Validação de CPF
- layout das telas (deixar mais estilizado)
