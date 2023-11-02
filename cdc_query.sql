--usar a base de dados que queremos
USE ContinentalTestDb
GO

--Implementar as Triggers para histórico de modificações
--Operation(1-delete, 2-insert, 3 update)

------------------------Stops
---DELETE
CREATE trigger Stop_DeleteLog ON Stops
  after delete
  as
        insert  into cdc_Stops
		([IdStop]
		   ,[Planned]
           ,[InitialDate]
           ,[EndDate]
           ,[Duration]
           ,[Shift]
           ,[LineId]
           ,[ReasonId]
           ,[ModificationDate]
           ,[Operation])
        select Id, Planned,InitialDate,EndDate,Duration,Shift,LineId,ReasonId,getdate(), 1 from deleted
  GO
---Insert
CREATE trigger Stop_InsertLog ON Stops
  after insert
  as
        insert  into cdc_Stops
		([IdStop]
		   ,[Planned]
           ,[InitialDate]
           ,[EndDate]
           ,[Duration]
           ,[Shift]
           ,[LineId]
           ,[ReasonId]
           ,[ModificationDate]
           ,[Operation])
        select Id, Planned,InitialDate,EndDate,Duration,Shift,LineId,ReasonId,getdate(), 2 from inserted
  GO
  --update
  CREATE trigger Stop_UpdateLog ON Stops
  after update
  as
        insert  into cdc_Stops
		([IdStop]
		   ,[Planned]
           ,[InitialDate]
           ,[EndDate]
           ,[Duration]
           ,[Shift]
           ,[LineId]
           ,[ReasonId]
           ,[ModificationDate]
           ,[Operation])
        select Id, Planned,InitialDate,EndDate,Duration,Shift,LineId,ReasonId,getdate(), 3 from inserted
  GO

------------------------Productions
    ---DELETE
 CREATE trigger Production_DeleteLog ON Productions
  after delete
  as
        insert  into cdc_Productions
		([IdProduction]
		   ,[Hour]
           ,[Day]
           ,[Quantity]
           ,[Production_PlanId]
           ,[ModificationDate]
           ,[Operation])
        select Id, Hour,Day,Quantity,Production_PlanId,getdate(), 1 from deleted
  GO

   ---Insert
 CREATE trigger Production_InsertLog ON Productions
  after insert
  as
        insert  into cdc_Productions
		([IdProduction]
		   ,[Hour]
           ,[Day]
           ,[Quantity]
           ,[Production_PlanId]
           ,[ModificationDate]
           ,[Operation])
        select Id, Hour,Day,Quantity,Production_PlanId,getdate(), 2 from inserted
  GO

     ---update
 CREATE trigger Production_UpdateLog ON Productions
  after update
  as
        insert  into cdc_Productions
		([IdProduction]
		   ,[Hour]
           ,[Day]
           ,[Quantity]
           ,[Production_PlanId]
           ,[ModificationDate]
           ,[Operation])
        select Id, Hour,Day,Quantity,Production_PlanId,getdate(), 3 from inserted
  GO
  -------------------------------------------------
  --Testar Stops
  DROP TRIGGER Stop_DeleteLog;
  DROP TRIGGER Stop_InsertLog;
  DROP TRIGGER Stop_UpdateLog;
  select * from Stops;
  select * from cdc_Stops;
  DELETE from cdc_Stops;

   --Testar Productions
  DROP TRIGGER Production_DeleteLog;
  DROP TRIGGER Production_UpdateLog;
  DROP TRIGGER Production_InsertLog;
  select * from Productions;
  select * from cdc_Productions;
  DELETE from cdc_Productions;



