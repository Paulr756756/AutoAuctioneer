drop procedure if exists update_part;
create procedure update_part(
    _id uuid,
    _name text,
    _description text,
    _category text,
    _marketprice bigint,
    _parttype int,
    _manufacturer text
)
language plpgsql as $$
begin
    update parts set
        name = _name,
        description = _description,
        category = _category,
        marketprice = _marketprice,
        parttype = _parttype,
        manufacturer  = _manufacturer 
    where id=_id;

    EXCEPTION
    WHEN others THEN
    RAISE EXCEPTION 'Error updating part : %', SQLERRM;
    
end;
$$;