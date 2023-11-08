drop procedure if exists delete_user;
create procedure delete_user(
    _id uuid
)
language plpgsql as $$
begin
    delete from users where id=_id;

    exception
        when others then
        raise exception 'Cannot delete user: %', SQLERRM;
end;
$$;
