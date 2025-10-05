ALTER TABLE Stories DROP CONSTRAINT [FK_Stories_Groups]

UPDATE StoriesPulls
SET GroupsIdGroup = NULL;

UPDATE StoriesPushes
SET GroupsIdGroup = NULL;

UPDATE Stories
SET GroupsIdGroup = NULL;

DELETE FROM Groups;

ALTER TABLE Groups ADD IdTipo INT NOT NULL; 