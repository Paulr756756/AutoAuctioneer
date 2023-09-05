drop procedure if exists insert_car;
create procedure insert_car(
    out _id uuid,
    _userId uuid,
    _type int,
    _make text,
    _model text,
    _year date,
    _color varchar(20),
    _vin text,
    _bodytype varchar(30),
    _fueltype varchar(20),
    _transmissiontype varchar(30),
    _enginetype varchar(20),
    _horsepower int,
    _torque int,
    _fuelefficiency real,
    _acceleration real,
    _topspeed int,
    _imageurls text[]
)
language plpgsql as $$
begin
    if _type = 0 then 
        _id := gen_random_uuid();
        insert into items (
            id,
            userid,
            type
        ) values (
            _id,
            _userid,
            _type
        );
        insert into cars (
            id,
            userid,
            make,
            model,
            year,
            color,
            vin,
            bodytype,
            fueltype,
            transmissiontype,
            enginetype,
            horsepower,
            torque,
            fuelefficiency,
            acceleration,
            topspeed,
            imageurls
        ) values (
            _id,
            _useruid,
            _make,
            _model,
            _year,
            _color,
            _vin,
            _bodytype,
            _fueltype,
            _transmissiontype,
            _enginetype,
            _horsepower,
            _torque,
            _fuelefficiency,
            _acceleration,
            _topspeed,
            _imageurls
        );
    else 
        raise exception 'Inconsistent item type w.r.t. the procedure.';
    end if;
end;
$$;

