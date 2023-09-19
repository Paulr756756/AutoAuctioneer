drop procedure if exists insert_bid;
create procedure insert_bid(
    out _id uuid,
    _userid uuid,
    _listingid uuid,
    _bidamount bigint,
    out _bidtime timestamp
)
language plpgsql as $$
begin
    _id := gen_random_uuid();
    _bidtime := CURRENT_TIMESTAMP AT TIME ZONE 'UTC';
    insert into bids (
        id,
        userid,
        listingid,
        bidamount,
        bidtime
    ) values (
        _id,
        _userid,
        _listingid,
        _bidamount,
        _bidtime
    );
end;
$$;