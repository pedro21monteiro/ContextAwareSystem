--usar a base de dados que queremos
USE ContinentalTestDb
GO

--Implementar as Triggers para histórico de modificações
--Operation(1-delete, 2-insert, 3 update)

--Components

CREATE trigger Component_Log ON Components
  after delete
  as
        insert  into cdc_Components
		(IdComponent,Name,Reference,Category,ModificationDate,Operation)
        select Id,Name,Reference,Category, getdate(), 1 from deleted
  GO

  DROP TRIGGER Component_Log;
  select * from Components;
  select * from cdc_Components;
  --CREATE trigger [dbo].[tr_i_Cliente_Log] ON [dbo].[Cliente]
  --after insert
  --as
  --      insert  Cliente_Log
  --      select *,system_user,getdate(),'I' from inserted

  --GO


  --CREATE trigger [dbo].[tr_u_Cliente_Log] ON [dbo].[Cliente]
  --after update
  --as
  --      insert  Cliente_Log
  --      select *,system_user,getdate(),'U' from deleted

  --GO

  -------------------------