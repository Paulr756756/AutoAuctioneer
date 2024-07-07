drop procedure if exists insert_listing;
create procedure insert_listing(
    out _id uuid,
    _userid uuid,
    _itemid uuid
)
    language plpgsql as $$
begin
    _id
:= gen_random_uuid();
insert into listings(id,
                     userid,
                     itemid)
values (_id,
        _userid,
        _itemid);
end;
$$;
