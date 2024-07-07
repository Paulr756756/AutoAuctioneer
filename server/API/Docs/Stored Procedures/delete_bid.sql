drop procedure if exists delete_bid;
create procedure delete_bid(
    _id uuid
)
    language plpgsql as $$
begin
delete
from bids
where id = _id;

exception
    when others THEN
    raise exception 'Error updating user: %', SQLERRM;
end;
$$;
