drop procedure if exists update_user;
create procedure update_user(
    inout _id uuid,
    _username varchar (25),
    _email varchar (50),
    _address text,
    _phoneno text,
    _firstname varchar (15),
    _lastname varchar (15),
    _dateofbirth date
)
    language plpgsql as $$
begin
update users
set username    = _username,
    email       = _email,
    address     = _address,
    phoneno     = _phoneno,
    firstname   = _firstname,
    lastname    = _lastname,
    dateofbirth = _dateofbirth
where id = _id;

EXCEPTION
    WHEN others THEN
    RAISE EXCEPTION 'Error updating user: %', SQLERRM;

end;
$$;