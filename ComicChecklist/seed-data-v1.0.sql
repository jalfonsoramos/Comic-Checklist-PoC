DECLARE @checklistid int;

INSERT INTO [dbo].[Checklists]([Name])VALUES('Dawn of X Volmen 1');
SET @checklistid = @@IDENTITY;
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('X-Men #1',@checklistid,0);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('Marauders #1',@checklistid,1);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('Excalibur #1',@checklistid,2);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('New Mutants #1',@checklistid,3);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('X-Force #1',@checklistid,4);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('Fallen Angels #1',@checklistid,5);

INSERT INTO [dbo].[Checklists]([Name])VALUES('Dawn of X Volmen 2');
SET @checklistid = @@IDENTITY;
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('X-Men #2',@checklistid,0);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('Marauders #2',@checklistid,1);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('Excalibur #2',@checklistid,2);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('New Mutants #2',@checklistid,3);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('X-Force #2',@checklistid,4);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('Fallen Angels #2',@checklistid,5);

INSERT INTO [dbo].[Checklists]([Name])VALUES('Dawn of X Volmen 3');
SET @checklistid = @@IDENTITY;
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('X-Men #3',@checklistid,0);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('Marauders #3',@checklistid,1);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('Excalibur #3',@checklistid,2);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('New Mutants #3',@checklistid,3);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('X-Force #3',@checklistid,4);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('Fallen Angels #3',@checklistid,5);

INSERT INTO [dbo].[Checklists]([Name])VALUES('Dawn of X Volmen 4');
SET @checklistid = @@IDENTITY;
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('X-Men #4',@checklistid,0);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('Marauders #4',@checklistid,1);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('Excalibur #4',@checklistid,2);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('New Mutants #4',@checklistid,3);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('X-Force #4',@checklistid,4);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('Fallen Angels #4',@checklistid,5);

INSERT INTO [dbo].[Checklists]([Name])VALUES('Dawn of X Volmen 5');
SET @checklistid = @@IDENTITY;
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('X-Men #5',@checklistid,0);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('Marauders #5',@checklistid,1);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('Excalibur #5',@checklistid,2);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('New Mutants #5',@checklistid,3);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('X-Force #5',@checklistid,4);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('Fallen Angels #5',@checklistid,5);

INSERT INTO [dbo].[Checklists]([Name])VALUES('Dawn of X Volmen 6');
SET @checklistid = @@IDENTITY;
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('X-Men #6',@checklistid,0);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('Marauders #6',@checklistid,1);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('Excalibur #6',@checklistid,2);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('New Mutants #6',@checklistid,3);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('X-Force #6',@checklistid,4);
INSERT INTO [dbo].[Issues]([Title],[ChecklistId],[Order])VALUES('Fallen Angels #6',@checklistid,5);



--delete Issues;
--delete Checklists;
select * from Checklists
select * from Issues


