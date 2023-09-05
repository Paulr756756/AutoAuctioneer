drop procedure if exists get_carsofsingleuser;
create procedure get_carsofsingleuser(
    _id uuid
)
language plpgsql as $$
begin
    select cars.* from cars inner join items
    on cars.id = items.id where
    items.userid = _id;
end;
$$;
