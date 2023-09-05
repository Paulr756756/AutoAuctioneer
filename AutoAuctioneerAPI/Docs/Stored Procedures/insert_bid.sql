drop procedure if exists insert_bid;
create procedure insert_bid(
    out _id uuid,
    _userid uuid,
    _bidamount bigint,
    _bidtime timestamp
)
language plpgsql as $$
begin
    _id := gen_random_uuid();
    insert into bids (
        id,
        userid,
        bidamount,
        bidtime
    ) values (
        _id,
        _userid,
        _bidamount,
        _bidtime
    );
end;
$$;