drop procedure if exists delete_item;
create procedure delete_item(
    _id uuid
)
language plpgsql as $$
begin
    delete from items where id=_id;
end;
$$;
