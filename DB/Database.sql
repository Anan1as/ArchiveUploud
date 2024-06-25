-- Active: 1719262487771@@bdriox2bzgcss1dpuaud-mysql.services.clever-cloud.com@3306
--Folders table
CREATE TABLE Folders
(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(255) NOT NULL,
    ParentFolderId INT,
    FOREIGN KEY (ParentFolderId) REFERENCES Folders(Id)
);

DROP TABLE Folders;


--Files table
CREATE TABLE Files
(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    FileName VARCHAR(255) NOT NULL,
    FilePath VARCHAR(255) NOT NULL,
    FolderId INT NOT NULL,
    FOREIGN KEY (FolderId) REFERENCES Folders(Id)
);

DROP TABLE Files;