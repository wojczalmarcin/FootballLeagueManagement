import React, { useEffect, useState } from 'react';
import moment from 'moment'
import { AddSeason, EditSeason, LoadSeasons } from '../../Services/SeasonService';

const Seasons = () => {

    const [loading, setLoading] = useState(true);

    const [seasons, setSeasons] = useState([]);

    const [editing, setEditing] = useState(0);

    const [adding, setAdding] = useState(false);

    const [inputs, setInputs] = useState({});

    const loadSeasonsData = () => {
        setLoading(true);
        LoadSeasons(setSeasons)
            .finally(() => {
                setLoading(false);
            });
    };

    const handleEdit = (season) => {
        setInputs(season);
        setEditing(true);
    };

    const handleCancelEdit = (season) => {
        setInputs(season);
        setEditing(false);
    };

    const handleSave = () => {
        EditSeason(inputs)
            .finally(() => {
                setEditing(false);
            });
    }

    const handleAdd = () => {
        setInputs({ startDate: "", endDate: "", sponsor: "" });
        setAdding(true);
    }

    const handlePost = () => {
        AddSeason(inputs)
            .finally(() => setAdding(false));
    }

    const handleCancelAdd = () => {
        setAdding(false);
    }

    const handleInputChange = (event) => {
        const name = event.target.name;
        const value = event.target.value;
        setInputs(values => ({ ...values, [name]: value }))
    };

    useEffect(() => {
        loadSeasonsData();
    }, []);

    useEffect(() => {
        if (editing === false) {
            loadSeasonsData();
        }
    }, [editing]);

    useEffect(() => {
        if (adding === false) {
            loadSeasonsData();
        }
    }, [adding]);

    const renderSeasons = () => {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Sezon</th>
                        <th>Data rozpoczęcia</th>
                        <th>Data zakończenia</th>
                        <th>Sponsor</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {seasons.map(season => {
                        if (season.id === inputs.id && editing) {
                            return (
                                <tr key={season.id}>
                                    <td>{new Date(season.startDate).getFullYear() + "/" + (season.endDate ? new Date(season.endDate).getFullYear() : "")}</td>
                                    <td>
                                        <input
                                            name="startDate"
                                            type="date"
                                            value={moment(new Date(inputs.startDate)).format("YYYY-MM-DD")}
                                            onChange={handleInputChange}
                                            format="DD.MM.yyyy"
                                        />
                                    </td>
                                    <td>
                                        <input
                                            name="endDate"
                                            type="date"
                                            value={inputs.endDate ? moment(new Date(inputs.endDate)).format("YYYY-MM-DD") : ""}
                                            onChange={handleInputChange}
                                            format="DD.MM.yyyy"
                                        />
                                    </td>
                                    <td>
                                        <input
                                            name="sponsor"
                                            type="text"
                                            value={inputs.sponsor ? inputs.sponsor : ""}
                                            onChange={handleInputChange}
                                        />
                                    </td>
                                    <td>
                                        <button className='btn-primary' onClick={handleSave}>Zapisz</button>
                                        <button className='btn-primary' onClick={() => handleCancelEdit(season)}>Anuluj</button>
                                    </td>
                                </tr>)
                        }
                        return (<tr key={season.id}>
                            <td>{new Date(season.startDate).getFullYear() + "/" + (season.endDate ? new Date(season.endDate).getFullYear() : "")}</td>
                            <td>
                                <input type="date" disabled={true} value={moment(new Date(season.startDate)).format("YYYY-MM-DD")} />
                            </td>
                            <td>
                                <input type="date" disabled={true} value={season.endDate ? moment(new Date(season.endDate)).format("YYYY-MM-DD") : ""} />
                            </td>
                            <td>
                                <input disabled={true} value={season.sponsor} />
                            </td>
                            <td>
                                <button className='btn-invisible' disabled={true}>Anuluj</button>
                                <button className='btn-primary' disabled={editing} onClick={() => handleEdit(season)}>Edytuj</button>
                            </td>
                        </tr>)
                    }
                    )}
                    {adding ?
                        <tr>
                            <td></td>
                            <td>
                                <input
                                    name="startDate"
                                    type="date"
                                    value={moment(new Date(inputs.startDate)).format("YYYY-MM-DD")}
                                    onChange={handleInputChange}
                                    format="DD.MM.yyyy"
                                />
                            </td>
                            <td>
                                <input
                                    name="endDate"
                                    type="date"
                                    value={inputs.endDate ? moment(new Date(inputs.endDate)).format("YYYY-MM-DD") : ""}
                                    onChange={handleInputChange}
                                    format="DD.MM.yyyy"
                                />
                            </td>
                            <td>
                                <input
                                    name="sponsor"
                                    type="text"
                                    value={inputs.sponsor ? inputs.sponsor : ""}
                                    onChange={handleInputChange}
                                />
                            </td>
                            <td>
                                <button className='btn-primary' disabled={editing} onClick={handlePost}>Dodaj</button>
                                <button className='btn-primary' disabled={editing} onClick={handleCancelAdd}>Anuluj</button>
                            </td>
                        </tr>
                        :
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>
                                <button className='btn-invisible' disabled={true}>Anuluj</button>
                                <button className='btn-primary' disabled={editing} onClick={handleAdd}>Dodaj</button>
                            </td>
                        </tr>}
                </tbody>
            </table>

        );
    }

    return (
        loading
            ? <p><em>Ładuję...</em></p>
            : renderSeasons()
    );
};

export default Seasons;