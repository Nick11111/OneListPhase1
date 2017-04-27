select * from list;
select * from listuser;
select * from liststatus;
select * from SuscriberGroupUser;
select * from SuscriberGroup;
select * from usertype;
select * from listitem;

update list set listname = 'Polish final project' where listID = '43';
delete from listuser where ListID = '40';
delete from listitem where ListItemID = '1032';

delete from list where ListName = 'Packing for France';