Robots_ServerConfig
-- Create table
create table Robots_ServerConfig
(
  id          NUMBER not null,
  servicename VARCHAR2(500),
  serviceip   VARCHAR2(100),
  lasttime    DATE,
  passminute  NUMBER,
  second      NUMBER
);
-- Create/Recreate indexes 
create index IDX_ROBOTS_SERVICECONFIG_NAME on Robots_ServerConfig (SERVICENAME);
-- Create/Recreate primary, unique and foreign key constraints 
alter table Robots_ServerConfig
  add primary key (ID);

  -- SEQ
  CREATE SEQUENCE ROBOTS_SERVERCONFIG_SEQ
     INCREMENT BY 1   -- 每次加几个  
     START WITH 1     -- 从1开始计数  
     NOMAXVALUE       -- 不设置最大值  ，设置最大值：maxvalue 9999
     NOCYCLE          -- 一直累加，不循环  
     CACHE 10; 

  -- trigger
  create or replace trigger "TIB_ROBOTS_SERVERCONFIG_SEQ" before insert
on Robots_ServerConfig for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
       select ROBOTS_SERVERCONFIG_SEQ.NEXTVAL INTO :NEW.id from dual;

--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
