drop procedure if exists delete_listing;
create procedure delete_listing(
    _id uuid
)
    language plpgsql as $$
begin
delete
from listings
where id = _id;

exception
    when others THEN
    raise exception 'Unable to delete listing: %', SQLERRM;
end;
$$;