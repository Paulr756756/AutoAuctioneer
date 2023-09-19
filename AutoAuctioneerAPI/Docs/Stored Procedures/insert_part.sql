drop procedure if exists insert_part;
create procedure insert_part(
    out _id uuid,
    _userid uuid,
    _type int,
    _name text,
    _description text,
    _category text,
    _marketprice bigint,
    _parttype int,
    _manufacturer text
)
language plpgsql as $$
begin
    if _type = 1 then 
        _id := gen_random_uuid();
        insert into items (
            id,
            userid,
            type
        ) values (
            _id,
            _userid,
            _type
        );
        insert into parts (
            id,
            name,
            description,
            category,
            marketprice,
            parttype,
            manufacturer
        ) values (
            _id,
            _name,
            _description,
            _category,
            _marketprice,
            _parttype,
            _manufacturer
        );
    else 
        raise exception 'Inconsistent item type w.r.t. the procedure.';
    end if;
end;
$$;