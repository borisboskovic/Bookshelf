/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2016                    */
/* Created on:     9/26/2021 3:42:26 PM                         */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Book') and o.name = 'FK_BOOK_ISPARTOFS_SERIES')
alter table Book
   drop constraint FK_BOOK_ISPARTOFS_SERIES
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BookAuthor') and o.name = 'FK_BOOKAUTH_AUTHOROF_BOOK')
alter table BookAuthor
   drop constraint FK_BOOKAUTH_AUTHOROF_BOOK
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BookAuthor') and o.name = 'FK_BOOKAUTH_WRITTENBY_AUTHOR')
alter table BookAuthor
   drop constraint FK_BOOKAUTH_WRITTENBY_AUTHOR
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BookEdition') and o.name = 'FK_BOOKEDIT_BYPUBLISH_PUBLISHE')
alter table BookEdition
   drop constraint FK_BOOKEDIT_BYPUBLISH_PUBLISHE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BookInReadingChallange') and o.name = 'FK_BOOKINRE_BOOKINCHA_READINGC')
alter table BookInReadingChallange
   drop constraint FK_BOOKINRE_BOOKINCHA_READINGC
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BookInReadingChallange') and o.name = 'FK_BOOKINRE_CHALLANGE_BOOKISSU')
alter table BookInReadingChallange
   drop constraint FK_BOOKINRE_CHALLANGE_BOOKISSU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BookIssue') and o.name = 'FK_BOOKISSU_ISSUEOF_BOOK')
alter table BookIssue
   drop constraint FK_BOOKISSU_ISSUEOF_BOOK
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BookIssue') and o.name = 'FK_BOOKISSU_LANGUAGEO_LANGUAGE')
alter table BookIssue
   drop constraint FK_BOOKISSU_LANGUAGEO_LANGUAGE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BookIssue') and o.name = 'FK_BOOKISSU_PUBLISHED_PUBLISHE')
alter table BookIssue
   drop constraint FK_BOOKISSU_PUBLISHED_PUBLISHE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BookIssueInEdition') and o.name = 'FK_BOOKISSU_EDITIONOF_BOOKEDIT')
alter table BookIssueInEdition
   drop constraint FK_BOOKISSU_EDITIONOF_BOOKEDIT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BookIssueInEdition') and o.name = 'FK_BOOKISSU_ISSUEINED_BOOKISSU')
alter table BookIssueInEdition
   drop constraint FK_BOOKISSU_ISSUEINED_BOOKISSU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BookOnBookshelf') and o.name = 'FK_BOOKONBO_BOOKONBOO_BOOKISSU')
alter table BookOnBookshelf
   drop constraint FK_BOOKONBO_BOOKONBOO_BOOKISSU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BookOnBookshelf') and o.name = 'FK_BOOKONBO_ONBOOKSHE_USERBOOK')
alter table BookOnBookshelf
   drop constraint FK_BOOKONBO_ONBOOKSHE_USERBOOK
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BookTag') and o.name = 'FK_BOOKTAG_BOOKHASTA_TAG')
alter table BookTag
   drop constraint FK_BOOKTAG_BOOKHASTA_TAG
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BookTag') and o.name = 'FK_BOOKTAG_TAGGEDBOO_BOOK')
alter table BookTag
   drop constraint FK_BOOKTAG_TAGGEDBOO_BOOK
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CommentReaction') and o.name = 'FK_COMMENTR_REACTIONO_REVIEWCO')
alter table CommentReaction
   drop constraint FK_COMMENTR_REACTIONO_REVIEWCO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CommentReaction') and o.name = 'FK_COMMENTR_REACTIONO_ASPNETUS')
alter table CommentReaction
   drop constraint FK_COMMENTR_REACTIONO_ASPNETUS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CurrentlyReading') and o.name = 'FK_CURRENTL_CURRENTLY_BOOKISSU')
alter table CurrentlyReading
   drop constraint FK_CURRENTL_CURRENTLY_BOOKISSU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CurrentlyReading') and o.name = 'FK_CURRENTL_CURRENTLY_ASPNETUS')
alter table CurrentlyReading
   drop constraint FK_CURRENTL_CURRENTLY_ASPNETUS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('"Read"') and o.name = 'FK_READ_READBOOK_BOOKISSU')
alter table "Read"
   drop constraint FK_READ_READBOOK_BOOKISSU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('"Read"') and o.name = 'FK_READ_READBY_ASPNETUS')
alter table "Read"
   drop constraint FK_READ_READBY_ASPNETUS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ReadingChallange') and o.name = 'FK_READINGC_CHALLANGE_YEAR')
alter table ReadingChallange
   drop constraint FK_READINGC_CHALLANGE_YEAR
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ReadingChallange') and o.name = 'FK_READINGC_OFUSER_ASPNETUS')
alter table ReadingChallange
   drop constraint FK_READINGC_OFUSER_ASPNETUS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Review') and o.name = 'FK_REVIEW_POSTEDBYU_ASPNETUS')
alter table Review
   drop constraint FK_REVIEW_POSTEDBYU_ASPNETUS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Review') and o.name = 'FK_REVIEW_REVIEWOF_BOOKISSU')
alter table Review
   drop constraint FK_REVIEW_REVIEWOF_BOOKISSU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ReviewComment') and o.name = 'FK_REVIEWCO_COMMENTBY_ASPNETUS')
alter table ReviewComment
   drop constraint FK_REVIEWCO_COMMENTBY_ASPNETUS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ReviewComment') and o.name = 'FK_REVIEWCO_COMMENTON_REVIEW')
alter table ReviewComment
   drop constraint FK_REVIEWCO_COMMENTON_REVIEW
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ReviewReaction') and o.name = 'FK_REVIEWRE_REACTEDON_REVIEW')
alter table ReviewReaction
   drop constraint FK_REVIEWRE_REACTEDON_REVIEW
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ReviewReaction') and o.name = 'FK_REVIEWRE_REACTEDON_ASPNETUS')
alter table ReviewReaction
   drop constraint FK_REVIEWRE_REACTEDON_ASPNETUS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('UserBookShelf') and o.name = 'FK_USERBOOK_BOOKSHELF_ASPNETUS')
alter table UserBookShelf
   drop constraint FK_USERBOOK_BOOKSHELF_ASPNETUS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WantToRead') and o.name = 'FK_WANTTORE_WANTTOREA_BOOKISSU')
alter table WantToRead
   drop constraint FK_WANTTORE_WANTTOREA_BOOKISSU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WantToRead') and o.name = 'FK_WANTTORE_WANTTOREA_ASPNETUS')
alter table WantToRead
   drop constraint FK_WANTTORE_WANTTOREA_ASPNETUS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Author')
            and   type = 'U')
   drop table Author
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Book')
            and   name  = 'ISPARTOFSERIES_FK'
            and   indid > 0
            and   indid < 255)
   drop index Book.ISPARTOFSERIES_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Book')
            and   type = 'U')
   drop table Book
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('BookAuthor')
            and   name  = 'WRITTENBY_FK'
            and   indid > 0
            and   indid < 255)
   drop index BookAuthor.WRITTENBY_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('BookAuthor')
            and   name  = 'AUTHOROF_FK'
            and   indid > 0
            and   indid < 255)
   drop index BookAuthor.AUTHOROF_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BookAuthor')
            and   type = 'U')
   drop table BookAuthor
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('BookEdition')
            and   name  = 'BYPUBLISHER_FK'
            and   indid > 0
            and   indid < 255)
   drop index BookEdition.BYPUBLISHER_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BookEdition')
            and   type = 'U')
   drop table BookEdition
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('BookInReadingChallange')
            and   name  = 'CHALLANGEISSUE_FK'
            and   indid > 0
            and   indid < 255)
   drop index BookInReadingChallange.CHALLANGEISSUE_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('BookInReadingChallange')
            and   name  = 'BOOKINCHALANGE_FK'
            and   indid > 0
            and   indid < 255)
   drop index BookInReadingChallange.BOOKINCHALANGE_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BookInReadingChallange')
            and   type = 'U')
   drop table BookInReadingChallange
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('BookIssue')
            and   name  = 'LANGUAGEOFISSUE_FK'
            and   indid > 0
            and   indid < 255)
   drop index BookIssue.LANGUAGEOFISSUE_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('BookIssue')
            and   name  = 'ISSUEOF_FK'
            and   indid > 0
            and   indid < 255)
   drop index BookIssue.ISSUEOF_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('BookIssue')
            and   name  = 'PUBLISHEDBY_FK'
            and   indid > 0
            and   indid < 255)
   drop index BookIssue.PUBLISHEDBY_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BookIssue')
            and   type = 'U')
   drop table BookIssue
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('BookIssueInEdition')
            and   name  = 'ISSUEINEDITION_FK'
            and   indid > 0
            and   indid < 255)
   drop index BookIssueInEdition.ISSUEINEDITION_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('BookIssueInEdition')
            and   name  = 'EDITIONOFISSE_FK'
            and   indid > 0
            and   indid < 255)
   drop index BookIssueInEdition.EDITIONOFISSE_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BookIssueInEdition')
            and   type = 'U')
   drop table BookIssueInEdition
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('BookOnBookshelf')
            and   name  = 'BOOKONBOOKSHELF_FK'
            and   indid > 0
            and   indid < 255)
   drop index BookOnBookshelf.BOOKONBOOKSHELF_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('BookOnBookshelf')
            and   name  = 'ONBOOKSHELF_FK'
            and   indid > 0
            and   indid < 255)
   drop index BookOnBookshelf.ONBOOKSHELF_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BookOnBookshelf')
            and   type = 'U')
   drop table BookOnBookshelf
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('BookTag')
            and   name  = 'BOOKHASTAG_FK'
            and   indid > 0
            and   indid < 255)
   drop index BookTag.BOOKHASTAG_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('BookTag')
            and   name  = 'TAGGEDBOOKIS_FK'
            and   indid > 0
            and   indid < 255)
   drop index BookTag.TAGGEDBOOKIS_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BookTag')
            and   type = 'U')
   drop table BookTag
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CommentReaction')
            and   name  = 'REACTIONONCOMMENTBY_FK'
            and   indid > 0
            and   indid < 255)
   drop index CommentReaction.REACTIONONCOMMENTBY_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CommentReaction')
            and   name  = 'REACTIONONCOMMENT_FK'
            and   indid > 0
            and   indid < 255)
   drop index CommentReaction.REACTIONONCOMMENT_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CommentReaction')
            and   type = 'U')
   drop table CommentReaction
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CurrentlyReading')
            and   name  = 'CURRENTLYREADINGBY_FK'
            and   indid > 0
            and   indid < 255)
   drop index CurrentlyReading.CURRENTLYREADINGBY_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CurrentlyReading')
            and   name  = 'CURRENTLYREADINGBOOK_FK'
            and   indid > 0
            and   indid < 255)
   drop index CurrentlyReading.CURRENTLYREADINGBOOK_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CurrentlyReading')
            and   type = 'U')
   drop table CurrentlyReading
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Language')
            and   type = 'U')
   drop table Language
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Publisher')
            and   type = 'U')
   drop table Publisher
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('"Read"')
            and   name  = 'READBOOK_FK'
            and   indid > 0
            and   indid < 255)
   drop index "Read".READBOOK_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('"Read"')
            and   name  = 'READBY_FK'
            and   indid > 0
            and   indid < 255)
   drop index "Read".READBY_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('"Read"')
            and   type = 'U')
   drop table "Read"
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ReadingChallange')
            and   name  = 'CHALLANGEFORYEAR_FK'
            and   indid > 0
            and   indid < 255)
   drop index ReadingChallange.CHALLANGEFORYEAR_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ReadingChallange')
            and   name  = 'OFUSER_FK'
            and   indid > 0
            and   indid < 255)
   drop index ReadingChallange.OFUSER_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ReadingChallange')
            and   type = 'U')
   drop table ReadingChallange
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Review')
            and   name  = 'REVIEWOF_FK'
            and   indid > 0
            and   indid < 255)
   drop index Review.REVIEWOF_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Review')
            and   name  = 'POSTEDBYUSER_FK'
            and   indid > 0
            and   indid < 255)
   drop index Review.POSTEDBYUSER_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Review')
            and   type = 'U')
   drop table Review
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ReviewComment')
            and   name  = 'COMMENTBY_FK'
            and   indid > 0
            and   indid < 255)
   drop index ReviewComment.COMMENTBY_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ReviewComment')
            and   name  = 'COMMENTON_FK'
            and   indid > 0
            and   indid < 255)
   drop index ReviewComment.COMMENTON_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ReviewComment')
            and   type = 'U')
   drop table ReviewComment
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ReviewReaction')
            and   name  = 'REACTEDONREVIEWBY_FK'
            and   indid > 0
            and   indid < 255)
   drop index ReviewReaction.REACTEDONREVIEWBY_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ReviewReaction')
            and   name  = 'REACTEDONREVIEW_FK'
            and   indid > 0
            and   indid < 255)
   drop index ReviewReaction.REACTEDONREVIEW_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ReviewReaction')
            and   type = 'U')
   drop table ReviewReaction
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Series')
            and   type = 'U')
   drop table Series
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Tag')
            and   type = 'U')
   drop table Tag
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('UserBookShelf')
            and   name  = 'BOOKSHELFBY_FK'
            and   indid > 0
            and   indid < 255)
   drop index UserBookShelf.BOOKSHELFBY_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('UserBookShelf')
            and   type = 'U')
   drop table UserBookShelf
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('WantToRead')
            and   name  = 'WANTTOREADBOOK_FK'
            and   indid > 0
            and   indid < 255)
   drop index WantToRead.WANTTOREADBOOK_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('WantToRead')
            and   name  = 'WANTTOREADBY_FK'
            and   indid > 0
            and   indid < 255)
   drop index WantToRead.WANTTOREADBY_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WantToRead')
            and   type = 'U')
   drop table WantToRead
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Year')
            and   type = 'U')
   drop table Year
go

/*==============================================================*/
/* Table: Author                                                */
/*==============================================================*/
create table Author (
   Id                   int                  identity,
   Name                 varchar(64)          not null,
   Surname              varchar(128)         not null,
   DateOfBirth          datetime             not null,
   DateOfDeath          datetime             null,
   ImageUrl             varchar(2000)        null,
   constraint PK_AUTHOR primary key (Id)
)
go

/*==============================================================*/
/* Table: Book                                                  */
/*==============================================================*/
create table Book (
   Id                   int                  identity,
   OriginalTitle        varchar(256)         not null,
   Series_Id            int                  null,
   OrderInSeries        int                  null,
   constraint PK_BOOK primary key (Id)
)
go

/*==============================================================*/
/* Index: ISPARTOFSERIES_FK                                     */
/*==============================================================*/




create nonclustered index ISPARTOFSERIES_FK on Book (Series_Id ASC)
go

/*==============================================================*/
/* Table: BookAuthor                                            */
/*==============================================================*/
create table BookAuthor (
   Author_Id            int                  not null,
   Book_Id              int                  not null,
   constraint PK_BOOKAUTHOR primary key (Author_Id, Book_Id)
)
go

/*==============================================================*/
/* Index: AUTHOROF_FK                                           */
/*==============================================================*/




create nonclustered index AUTHOROF_FK on BookAuthor (Book_Id ASC)
go

/*==============================================================*/
/* Index: WRITTENBY_FK                                          */
/*==============================================================*/




create nonclustered index WRITTENBY_FK on BookAuthor (Author_Id ASC)
go

/*==============================================================*/
/* Table: BookEdition                                           */
/*==============================================================*/
create table BookEdition (
   Id                   int                  identity,
   Publisher_Id         int                  not null,
   Name                 varchar(100)         not null,
   Description          varchar(4000)        null,
   ImageUrl             varchar(200)         null,
   constraint PK_BOOKEDITION primary key (Id)
)
go

/*==============================================================*/
/* Index: BYPUBLISHER_FK                                        */
/*==============================================================*/




create nonclustered index BYPUBLISHER_FK on BookEdition (Publisher_Id ASC)
go

/*==============================================================*/
/* Table: BookInReadingChallange                                */
/*==============================================================*/
create table BookInReadingChallange (
   Book_Id              int                  not null,
   BookIssue_Id         int                  not null,
   ReadingChallange_Id  int                  not null,
   FinishedOn           datetime             not null,
   UserComment          varchar(4000)        null,
   constraint PK_BOOKINREADINGCHALLANGE primary key (Book_Id, BookIssue_Id, ReadingChallange_Id)
)
go

/*==============================================================*/
/* Index: BOOKINCHALANGE_FK                                     */
/*==============================================================*/




create nonclustered index BOOKINCHALANGE_FK on BookInReadingChallange (ReadingChallange_Id ASC)
go

/*==============================================================*/
/* Index: CHALLANGEISSUE_FK                                     */
/*==============================================================*/




create nonclustered index CHALLANGEISSUE_FK on BookInReadingChallange (Book_Id ASC,
  BookIssue_Id ASC)
go

/*==============================================================*/
/* Table: BookIssue                                             */
/*==============================================================*/
create table BookIssue (
   Id                   int                  identity,
   Book_Id              int                  not null,
   Publisher_Id         int                  not null,
   Language_Id          int                  not null,
   ISBN                 varchar(10)          null,
   ISBN13               varchar(13)          null,
   Summary              varchar(4000)        null,
   PublishedOn          datetime             null,
   ImageUrl             varchar(2000)        null,
   constraint PK_BOOKISSUE primary key (Book_Id, Id)
)
go

/*==============================================================*/
/* Index: PUBLISHEDBY_FK                                        */
/*==============================================================*/




create nonclustered index PUBLISHEDBY_FK on BookIssue (Publisher_Id ASC)
go

/*==============================================================*/
/* Index: ISSUEOF_FK                                            */
/*==============================================================*/




create nonclustered index ISSUEOF_FK on BookIssue (Book_Id ASC)
go

/*==============================================================*/
/* Index: LANGUAGEOFISSUE_FK                                    */
/*==============================================================*/




create nonclustered index LANGUAGEOFISSUE_FK on BookIssue (Language_Id ASC)
go

/*==============================================================*/
/* Table: BookIssueInEdition                                    */
/*==============================================================*/
create table BookIssueInEdition (
   Edition_Id           int                  not null,
   Book_Id              int                  not null,
   BookIssue_id         int                  not null,
   OrderInEdition       int                  not null,
   PublishedOn          datetime             null,
   constraint PK_BOOKISSUEINEDITION primary key (Book_Id, BookIssue_id, Edition_Id)
)
go

/*==============================================================*/
/* Index: EDITIONOFISSE_FK                                      */
/*==============================================================*/




create nonclustered index EDITIONOFISSE_FK on BookIssueInEdition (Edition_Id ASC)
go

/*==============================================================*/
/* Index: ISSUEINEDITION_FK                                     */
/*==============================================================*/




create nonclustered index ISSUEINEDITION_FK on BookIssueInEdition (Book_Id ASC,
  BookIssue_id ASC)
go

/*==============================================================*/
/* Table: BookOnBookshelf                                       */
/*==============================================================*/
create table BookOnBookshelf (
   User_Id              varchar(450)         not null,
   BookShelf_Id         int                  not null,
   Book_Id              int                  not null,
   BookIssue_Id         int                  not null,
   DateAdded            datetime             not null,
   Comment              varchar(4000)        null,
   constraint PK_BOOKONBOOKSHELF primary key (User_Id, BookShelf_Id, Book_Id, BookIssue_Id)
)
go

/*==============================================================*/
/* Index: ONBOOKSHELF_FK                                        */
/*==============================================================*/




create nonclustered index ONBOOKSHELF_FK on BookOnBookshelf (User_Id ASC,
  BookShelf_Id ASC)
go

/*==============================================================*/
/* Index: BOOKONBOOKSHELF_FK                                    */
/*==============================================================*/




create nonclustered index BOOKONBOOKSHELF_FK on BookOnBookshelf (Book_Id ASC,
  BookIssue_Id ASC)
go

/*==============================================================*/
/* Table: BookTag                                               */
/*==============================================================*/
create table BookTag (
   Book_Id              int                  not null,
   Tag_Id               int                  not null,
   constraint PK_BOOKTAG primary key (Tag_Id, Book_Id)
)
go

/*==============================================================*/
/* Index: TAGGEDBOOKIS_FK                                       */
/*==============================================================*/




create nonclustered index TAGGEDBOOKIS_FK on BookTag (Book_Id ASC)
go

/*==============================================================*/
/* Index: BOOKHASTAG_FK                                         */
/*==============================================================*/




create nonclustered index BOOKHASTAG_FK on BookTag (Tag_Id ASC)
go

/*==============================================================*/
/* Table: CommentReaction                                       */
/*==============================================================*/
create table CommentReaction (
   User_Id              varchar(450)         not null,
   Book_Id              int                  not null,
   BookIssue_Id         int                  not null,
   Review_User_Id       varchar(450)         not null,
   CommentAuthor_Id     varchar(450)         not null,
   "Like"               bit                  not null,
   constraint PK_COMMENTREACTION primary key (User_Id, Book_Id, BookIssue_Id, Review_User_Id, CommentAuthor_Id)
)
go

/*==============================================================*/
/* Index: REACTIONONCOMMENT_FK                                  */
/*==============================================================*/




create nonclustered index REACTIONONCOMMENT_FK on CommentReaction (Book_Id ASC,
  BookIssue_Id ASC,
  Review_User_Id ASC,
  CommentAuthor_Id ASC)
go

/*==============================================================*/
/* Index: REACTIONONCOMMENTBY_FK                                */
/*==============================================================*/




create nonclustered index REACTIONONCOMMENTBY_FK on CommentReaction (User_Id ASC)
go

/*==============================================================*/
/* Table: CurrentlyReading                                      */
/*==============================================================*/
create table CurrentlyReading (
   User_Id              varchar(450)         not null,
   Book_Id              int                  not null,
   BookIssue_Id         int                  not null,
   AddedOn              datetime             not null,
   PagesRead            int                  null,
   constraint PK_CURRENTLYREADING primary key (User_Id, Book_Id, BookIssue_Id)
)
go

/*==============================================================*/
/* Index: CURRENTLYREADINGBOOK_FK                               */
/*==============================================================*/




create nonclustered index CURRENTLYREADINGBOOK_FK on CurrentlyReading (Book_Id ASC,
  BookIssue_Id ASC)
go

/*==============================================================*/
/* Index: CURRENTLYREADINGBY_FK                                 */
/*==============================================================*/




create nonclustered index CURRENTLYREADINGBY_FK on CurrentlyReading (User_Id ASC)
go

/*==============================================================*/
/* Table: Language                                              */
/*==============================================================*/
create table Language (
   Id                   int                  identity,
   EnglishName          varchar(100)         not null,
   NativeName           varchar(200)         not null,
   LocalizationCode     varchar(10)          not null,
   constraint PK_LANGUAGE primary key (Id)
)
go

/*==============================================================*/
/* Table: Publisher                                             */
/*==============================================================*/
create table Publisher (
   Id                   int                  identity,
   Name                 varchar(512)         not null,
   ImageUrl             varchar(2000)        null,
   Address              varchar(200)         null,
   constraint PK_PUBLISHER primary key (Id)
)
go

/*==============================================================*/
/* Table: "Read"                                                */
/*==============================================================*/
create table "Read" (
   User_Id              varchar(450)         not null,
   Book_Id              int                  not null,
   BookIssue_Id         int                  not null,
   AddedOn              datetime             not null,
   StartedOn            datetime             null,
   FinishedOn           datetime             null,
   constraint PK_READ primary key (Book_Id, BookIssue_Id, User_Id)
)
go

/*==============================================================*/
/* Index: READBY_FK                                             */
/*==============================================================*/




create nonclustered index READBY_FK on "Read" (User_Id ASC)
go

/*==============================================================*/
/* Index: READBOOK_FK                                           */
/*==============================================================*/




create nonclustered index READBOOK_FK on "Read" (Book_Id ASC,
  BookIssue_Id ASC)
go

/*==============================================================*/
/* Table: ReadingChallange                                      */
/*==============================================================*/
create table ReadingChallange (
   Id                   int                  identity,
   User_Id              varchar(450)         not null,
   Year_Id              int                  not null,
   BooksPledged         int                  not null,
   constraint PK_READINGCHALLANGE primary key (Id)
)
go

/*==============================================================*/
/* Index: OFUSER_FK                                             */
/*==============================================================*/




create nonclustered index OFUSER_FK on ReadingChallange (User_Id ASC)
go

/*==============================================================*/
/* Index: CHALLANGEFORYEAR_FK                                   */
/*==============================================================*/




create nonclustered index CHALLANGEFORYEAR_FK on ReadingChallange (Year_Id ASC)
go

/*==============================================================*/
/* Table: Review                                                */
/*==============================================================*/
create table Review (
   Book_Id              int                  not null,
   BookIssue_Id         int                  not null,
   User_Id              varchar(450)         not null,
   Rating               int                  null,
   Review               varchar(4000)        null,
   PostedOn             datetime             not null,
   constraint PK_REVIEW primary key (Book_Id, BookIssue_Id, User_Id)
)
go

/*==============================================================*/
/* Index: POSTEDBYUSER_FK                                       */
/*==============================================================*/




create nonclustered index POSTEDBYUSER_FK on Review (User_Id ASC)
go

/*==============================================================*/
/* Index: REVIEWOF_FK                                           */
/*==============================================================*/




create nonclustered index REVIEWOF_FK on Review (Book_Id ASC,
  BookIssue_Id ASC)
go

/*==============================================================*/
/* Table: ReviewComment                                         */
/*==============================================================*/
create table ReviewComment (
   Book_Id              int                  not null,
   BookIssue_Id         int                  not null,
   Review_User_Id       varchar(450)         not null,
   CommentAuthor_Id     varchar(450)         not null,
   Content              varchar(4000)        null,
   constraint PK_REVIEWCOMMENT primary key (Book_Id, BookIssue_Id, Review_User_Id, CommentAuthor_Id)
)
go

/*==============================================================*/
/* Index: COMMENTON_FK                                          */
/*==============================================================*/




create nonclustered index COMMENTON_FK on ReviewComment (Book_Id ASC,
  BookIssue_Id ASC,
  Review_User_Id ASC)
go

/*==============================================================*/
/* Index: COMMENTBY_FK                                          */
/*==============================================================*/




create nonclustered index COMMENTBY_FK on ReviewComment (CommentAuthor_Id ASC)
go

/*==============================================================*/
/* Table: ReviewReaction                                        */
/*==============================================================*/
create table ReviewReaction (
   User_Id              varchar(450)         not null,
   Book_Id              int                  not null,
   BookIssue_Id         int                  not null,
   Review_User_Id       varchar(450)         not null,
   "Like"               bit                  not null,
   constraint PK_REVIEWREACTION primary key (User_Id, Book_Id, BookIssue_Id, Review_User_Id)
)
go

/*==============================================================*/
/* Index: REACTEDONREVIEW_FK                                    */
/*==============================================================*/




create nonclustered index REACTEDONREVIEW_FK on ReviewReaction (Book_Id ASC,
  BookIssue_Id ASC,
  Review_User_Id ASC)
go

/*==============================================================*/
/* Index: REACTEDONREVIEWBY_FK                                  */
/*==============================================================*/




create nonclustered index REACTEDONREVIEWBY_FK on ReviewReaction (User_Id ASC)
go

/*==============================================================*/
/* Table: Series                                                */
/*==============================================================*/
create table Series (
   Id                   int                  identity,
   Name                 varchar(512)         not null,
   Description          varchar(4000)        null,
   ImageUrl             varchar(200)         null,
   constraint PK_SERIES primary key (Id)
)
go

/*==============================================================*/
/* Table: Tag                                                   */
/*==============================================================*/
create table Tag (
   Id                   int                  identity,
   Name                 varchar(50)          not null,
   Description          varchar(2000)        null,
   constraint PK_TAG primary key (Id)
)
go

/*==============================================================*/
/* Table: UserBookShelf                                         */
/*==============================================================*/
create table UserBookShelf (
   Id                   int                  identity,
   User_Id              varchar(450)         not null,
   Name                 varchar(256)         not null,
   Descripton           varchar(4000)        null,
   "Public"             bit                  not null,
   CreatedOn            datetime             not null,
   constraint PK_USERBOOKSHELF primary key (User_Id, Id)
)
go

/*==============================================================*/
/* Index: BOOKSHELFBY_FK                                        */
/*==============================================================*/




create nonclustered index BOOKSHELFBY_FK on UserBookShelf (User_Id ASC)
go

/*==============================================================*/
/* Table: WantToRead                                            */
/*==============================================================*/
create table WantToRead (
   User_Id              varchar(450)         not null,
   Book_Id              int                  not null,
   BookIsse_Id          int                  not null,
   AddedOn              datetime             not null,
   constraint PK_WANTTOREAD primary key (Book_Id, BookIsse_Id, User_Id)
)
go

/*==============================================================*/
/* Index: WANTTOREADBY_FK                                       */
/*==============================================================*/




create nonclustered index WANTTOREADBY_FK on WantToRead (User_Id ASC)
go

/*==============================================================*/
/* Index: WANTTOREADBOOK_FK                                     */
/*==============================================================*/




create nonclustered index WANTTOREADBOOK_FK on WantToRead (Book_Id ASC,
  BookIsse_Id ASC)
go

/*==============================================================*/
/* Table: Year                                                  */
/*==============================================================*/
create table Year (
   Id                   int                  identity,
   Year                 int                  not null,
   constraint PK_YEAR primary key (Id)
)
go

alter table Book
   add constraint FK_BOOK_ISPARTOFS_SERIES foreign key (Series_Id)
      references Series (Id)
go

alter table BookAuthor
   add constraint FK_BOOKAUTH_AUTHOROF_BOOK foreign key (Book_Id)
      references Book (Id)
go

alter table BookAuthor
   add constraint FK_BOOKAUTH_WRITTENBY_AUTHOR foreign key (Author_Id)
      references Author (Id)
go

alter table BookEdition
   add constraint FK_BOOKEDIT_BYPUBLISH_PUBLISHE foreign key (Publisher_Id)
      references Publisher (Id)
go

alter table BookInReadingChallange
   add constraint FK_BOOKINRE_BOOKINCHA_READINGC foreign key (ReadingChallange_Id)
      references ReadingChallange (Id)
go

alter table BookInReadingChallange
   add constraint FK_BOOKINRE_CHALLANGE_BOOKISSU foreign key (Book_Id, BookIssue_Id)
      references BookIssue (Book_Id, Id)
go

alter table BookIssue
   add constraint FK_BOOKISSU_ISSUEOF_BOOK foreign key (Book_Id)
      references Book (Id)
go

alter table BookIssue
   add constraint FK_BOOKISSU_LANGUAGEO_LANGUAGE foreign key (Language_Id)
      references Language (Id)
go

alter table BookIssue
   add constraint FK_BOOKISSU_PUBLISHED_PUBLISHE foreign key (Publisher_Id)
      references Publisher (Id)
go

alter table BookIssueInEdition
   add constraint FK_BOOKISSU_EDITIONOF_BOOKEDIT foreign key (Edition_Id)
      references BookEdition (Id)
go

alter table BookIssueInEdition
   add constraint FK_BOOKISSU_ISSUEINED_BOOKISSU foreign key (Book_Id, BookIssue_id)
      references BookIssue (Book_Id, Id)
go

alter table BookOnBookshelf
   add constraint FK_BOOKONBO_BOOKONBOO_BOOKISSU foreign key (Book_Id, BookIssue_Id)
      references BookIssue (Book_Id, Id)
go

alter table BookOnBookshelf
   add constraint FK_BOOKONBO_ONBOOKSHE_USERBOOK foreign key (User_Id, BookShelf_Id)
      references UserBookShelf (User_Id, Id)
go

alter table BookTag
   add constraint FK_BOOKTAG_BOOKHASTA_TAG foreign key (Tag_Id)
      references Tag (Id)
go

alter table BookTag
   add constraint FK_BOOKTAG_TAGGEDBOO_BOOK foreign key (Book_Id)
      references Book (Id)
go

alter table CommentReaction
   add constraint FK_COMMENTR_REACTIONO_REVIEWCO foreign key (Book_Id, BookIssue_Id, Review_User_Id, CommentAuthor_Id)
      references ReviewComment (Book_Id, BookIssue_Id, Review_User_Id, CommentAuthor_Id)
go

alter table CommentReaction
   add constraint FK_COMMENTR_REACTIONO_ASPNETUS foreign key (User_Id)
      references AspNetUsers (Id)
go

alter table CurrentlyReading
   add constraint FK_CURRENTL_CURRENTLY_BOOKISSU foreign key (Book_Id, BookIssue_Id)
      references BookIssue (Book_Id, Id)
go

alter table CurrentlyReading
   add constraint FK_CURRENTL_CURRENTLY_ASPNETUS foreign key (User_Id)
      references AspNetUsers (Id)
go

alter table "Read"
   add constraint FK_READ_READBOOK_BOOKISSU foreign key (Book_Id, BookIssue_Id)
      references BookIssue (Book_Id, Id)
go

alter table "Read"
   add constraint FK_READ_READBY_ASPNETUS foreign key (User_Id)
      references AspNetUsers (Id)
go

alter table ReadingChallange
   add constraint FK_READINGC_CHALLANGE_YEAR foreign key (Year_Id)
      references Year (Id)
go

alter table ReadingChallange
   add constraint FK_READINGC_OFUSER_ASPNETUS foreign key (User_Id)
      references AspNetUsers (Id)
go

alter table Review
   add constraint FK_REVIEW_POSTEDBYU_ASPNETUS foreign key (User_Id)
      references AspNetUsers (Id)
go

alter table Review
   add constraint FK_REVIEW_REVIEWOF_BOOKISSU foreign key (Book_Id, BookIssue_Id)
      references BookIssue (Book_Id, Id)
go

alter table ReviewComment
   add constraint FK_REVIEWCO_COMMENTBY_ASPNETUS foreign key (CommentAuthor_Id)
      references AspNetUsers (Id)
go

alter table ReviewComment
   add constraint FK_REVIEWCO_COMMENTON_REVIEW foreign key (Book_Id, BookIssue_Id, Review_User_Id)
      references Review (Book_Id, BookIssue_Id, User_Id)
go

alter table ReviewReaction
   add constraint FK_REVIEWRE_REACTEDON_REVIEW foreign key (Book_Id, BookIssue_Id, Review_User_Id)
      references Review (Book_Id, BookIssue_Id, User_Id)
go

alter table ReviewReaction
   add constraint FK_REVIEWRE_REACTEDON_ASPNETUS foreign key (User_Id)
      references AspNetUsers (Id)
go

alter table UserBookShelf
   add constraint FK_USERBOOK_BOOKSHELF_ASPNETUS foreign key (User_Id)
      references AspNetUsers (Id)
go

alter table WantToRead
   add constraint FK_WANTTORE_WANTTOREA_BOOKISSU foreign key (Book_Id, BookIsse_Id)
      references BookIssue (Book_Id, Id)
go

alter table WantToRead
   add constraint FK_WANTTORE_WANTTOREA_ASPNETUS foreign key (User_Id)
      references AspNetUsers (Id)
go

