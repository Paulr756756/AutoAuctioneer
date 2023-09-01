create procedure insert_user(
    out _id uuid,
    _username varchar(25),
    _email varchar(50),
    _verificationtoken text,
    _address text,
    _registeredat date,
    _phoneno text,
    _firstname varchar(15),
    _lastname varchar(15),
    _dateofbirth date,
    _passwordhash text
)
language plpgsql as $$
begin
    _id := gen_random_uuid();
    insert into users (
        id,
        username,
        email,
        verificationtoken,
        address,
        registeredat,
        phoneno,
        firstname,
        lastname,
        dateofbirth,
        passwordhash
    ) values (
        _id,
        _username,
        _email,
        _verificationtoken,
        _address,
        _registeredat,
        _phoneno,
        _firstname,
        _lastname,
        _dateofbirth,
        _passwordhash
    );
end;
$$;