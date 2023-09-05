drop procedure if exists get_partsofsingleuser;
create procedure get_partsofsingleuser(
    _id uuid
)
language plpgsql as $$
begin
    select parts.* from parts inner join items
    on parts.id = items.id where
    items.userid = _id;
end;
$$;
