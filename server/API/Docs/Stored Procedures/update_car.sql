drop procedure if exists update_car;
create procedure update_car(
    _id uuid,
    _make text,
    _model text,
    _year date,
    _color varchar (20),
    _vin text,
    _bodytype varchar (30),
    _fueltype varchar (20),
    _transmissiontype varchar (30),
    _enginetype varchar (20),
    _horsepower int,
    _torque int,
    _fuelefficiency real,
    _acceleration real,
    _topspeed int,
    _imageurls text[]
)
    language plpgsql as $$
begin
update cars
set make             = _make,
    model            = _model,
    year             = _year,
    color            = _color,
    vin              = _vin,
    bodytype         = _bodytype,
    fueltype         = _fueltype,
    transmissiontype = _transmissiontype,
    enginetype       = _enginetype,
    horsepower       = _horsepower,
    torque           = _torque,
    fuelefficiency   = _fuelefficiency,
    acceleration     = _acceleration,
    topspeed         = _topspeed,
    imageurls        = _imageurls
where id = _id;

EXCEPTION
    WHEN others THEN
    RAISE EXCEPTION 'Error updating car: %', SQLERRM;

end;
$$;