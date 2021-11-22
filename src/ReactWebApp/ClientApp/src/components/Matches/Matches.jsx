import React, { useEffect, useState } from 'react';
import Dropdown from '../SeasonDropdown/Dropdown';
import { useHistory } from "react-router-dom";
import TeamPicker from './Pickers/TeamPicker';
import { LoadTeams } from '../../Services/TeamService';
import moment from 'moment'
import { LoadSeasonsCurrentSeason } from '../../Services/SeasonService';
import { AddMatch, DeleteMatch, EditMatch, LoadMatchesBySeasonId } from '../../Services/MatchService';

const Matches = () => {
    const [loading, setLoading] = useState(true);

    const [matches, setMatches] = useState([]);

    const [seasons, setSeasons] = useState([]);

    const [editing, setEditing] = useState(0);

    const [deleting, setDeleting] = useState(false);

    const [adding, setAdding] = useState(false);

    const [inputs, setInputs] = useState({});

    const [currentSeason, setCurrentSeason] = useState({});

    const [teams, setTeams] = useState([]);

    const [teamHome, setTeamHome] = useState({});

    const [teamAway, setTeamAway] = useState({});

    const history = useHistory();

    const loadSeasons = () => {
        setLoading(true);
        LoadSeasonsCurrentSeason(setSeasons, setCurrentSeason);
    };

    const loadMatches = () => {
        setLoading(true);
        LoadTeams(setTeams, currentSeason.id).then(() => {
            LoadMatchesBySeasonId(setMatches, currentSeason.id);
        })
            .finally(() => {
                setLoading(false);
            });
    }

    const handleStats = (matchId) => {
        history.push(`/match-stats/${matchId}`);
    };

    const handleDelete = (matchId) => {
        setDeleting(true);
        DeleteMatch(matchId)
            .finally(() => setDeleting(false))
    }

    const handleEdit = (match) => {
        setInputs(match);
        setEditing(match.id);
    };

    const handleCancelEdit = (match) => {
        setInputs(match);
        setEditing(0);
    };

    const handleSave = () => {
        EditMatch(inputs)
            .finally(() => {
                setEditing(0);
            });
    }

    const handleAdd = () => {
        setAdding(true);
    }

    const handlePost = () => {
        inputs.teamHomeId = teamHome.id;
        inputs.teamAwayId = teamAway.id;
        inputs.seasonId = currentSeason.id;
        AddMatch(inputs)
            .finally(() => setAdding(false));
    }

    const handleCancelAdd = () => {
        setAdding(false);
    }

    const handleInputChange = (event) => {
        const name = event.target.name;
        var value = event.target.value;
        if(name === "date" && !value)
        {
            value = null;
        }
        setInputs(values => ({ ...values, [name]: value }))
    };

    useEffect(() => {
        loadSeasons();
    }, []);

    useEffect(() => {
        if (currentSeason.id) {
            loadMatches();
        }
    }, [currentSeason]);

    useEffect(() => {
        if (editing === 0 && currentSeason.id) {
            loadMatches();
        }
    }, [editing]);

    useEffect(() => {
        if (adding === false && currentSeason.id) {
            loadMatches();
        }
    }, [adding]);

    useEffect(() => {
        if (currentSeason.id) {
            loadMatches();
        }
    }, [deleting]);

    const renderMatches = () => {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Gospodarz</th>
                        <th>Gość</th>
                        <th>Wynik</th>
                        <th>Data</th>
                        <th>Adres</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {matches.map(match => {
                        return (
                            <tr key={match.id}>
                                <td>{match.teamHome.name}</td>
                                <td>{match.teamAway.name}</td>
                                <td>{match.matchScore && match.isFinished ? match.matchScore.homeGoals + ":" + match.matchScore.awayGoals : ""}</td>
                                {editing !== match.id ?
                                    <td>{match.date ? moment(new Date(match.date)).format("DD.MM.YYYY") : ""}</td>
                                    :
                                    <td>
                                        <input
                                            name="date"
                                            type="date"
                                            value={inputs.date ? moment(new Date(inputs.date)).format("YYYY-MM-DD") : ""}
                                            onChange={handleInputChange}
                                            format="DD.MM.yyyy"
                                        />
                                    </td>
                                }
                                <td>{match.address ? match.address.city + " " + match.address.street + " " + match.address.houseNumber : ""}</td>
                                {editing !== match.id ?
                                    <td>
                                        <button className='btn-primary' onClick={() => handleStats(match.id)}>Statystyki</button>
                                        <button className='btn-primary' onClick={() => handleDelete(match.id)} disabled={match.isFinished}>Usuń</button>
                                        <button className='btn-primary' onClick={() => handleEdit(match)} disabled={match.isFinished}>Edytuj</button>              
                                    </td>
                                    :
                                    <td>
                                        <button className='btn-invisible' disabled={true}></button>
                                        <button className='btn-primary' onClick={() => handleSave()}>Zapisz</button>
                                        <button className='btn-primary' onClick={() => handleCancelEdit(match)}>Anuluj</button>
                                    </td>
                                }
                            </tr>
                        )
                    }
                    )}
                    {adding ?
                        <tr>
                            <td><TeamPicker teams={teams} setCurrentTeam={setTeamHome} team={teamHome} /></td>
                            <td><TeamPicker teams={teams} setCurrentTeam={setTeamAway} team={teamAway} /></td>
                            <td></td>
                            <td>
                                <input
                                    name="date"
                                    type="date"
                                    value={inputs.date ? moment(new Date(inputs.date)).format("YYYY-MM-DD") : ""}
                                    onChange={handleInputChange}
                                    format="DD.MM.yyyy"
                                />
                            </td>
                            <td></td>
                            <td>
                                <button className='btn-primary' disabled={editing} onClick={handleCancelAdd}>Anuluj</button>
                                <button className='btn-primary' disabled={editing} onClick={handlePost}>Dodaj</button>
                                <button className='btn-invisible' disabled={true}></button>
                            </td>
                        </tr>
                        :
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>
                                <button className='btn-primary' disabled={editing} onClick={handleAdd}>Dodaj</button>
                                <button className='btn-invisible' disabled={true}></button>
                                <button className='btn-invisible' disabled={true}></button>
                            </td>
                        </tr>
                    }
                </tbody>
            </table>

        );
    }

    return (
        loading
            ? <p><em>Ładuję...</em></p>
            :
            <div>
                <Dropdown seasons={seasons} currentSeason={currentSeason} setCurrentSeason={setCurrentSeason} />
                {renderMatches()}
            </div>
    );
}

export default Matches;