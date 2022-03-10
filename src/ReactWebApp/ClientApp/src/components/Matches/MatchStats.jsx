import React, { useEffect, useState } from 'react';
import { useParams } from "react-router-dom";
import { AddPlayerStat, DeletePlayerStat, LoadMatchStats, LoadStatTypes } from '../../Services/PlayerStatsService';
import { LoadPlayersByMatchId } from '../../Services/MemberService';
import { LoadMatchById } from '../../Services/MatchService';
import TeamPicker from './Pickers/TeamPicker';
import StatTypePicker from './Pickers/StatTypePicker';
import PlayerPicker from './Pickers/PlayerPicker';

const MatchStats = () => {

    const { matchId } = useParams();

    const [loading, setLoading] = useState(true);

    const [matchStats, setMatchStats] = useState([]);

    const [adding, setAdding] = useState(false);

    const [players, setPlayers] = useState([]);

    const [team, setTeam] = useState({});

    const [match, setMatch] = useState({});

    const [statTypes, setStatTypes] = useState([]);

    const [statType, setStatType] = useState({});

    const [player, setPlayer] = useState({});

    const [minute, setMinute] = useState(0);

    const [deleting, setDeleting] = useState(false);

    const loadPickersData = () => {
        setAdding(false);
        LoadPlayersByMatchId(setPlayers, matchId)
            .then(() => LoadStatTypes(setStatTypes))
            .then(() => LoadMatchById(setMatch, matchId));
    }

    const loadStats = () => {
        setLoading(true);
        LoadMatchStats(setMatchStats, matchId)
            .finally(() => {
                setLoading(false);
            });
    }

    const handleAdd = () => {
        loadPickersData();
    }

    const handlePost = () => {
        var inputs = {};
        inputs.matchId = matchId;
        inputs.playerId = player.id;
        inputs.teamId = team.id;
        inputs.statTypeId = statType.id;
        inputs.startMinute = minute;
        AddPlayerStat(inputs)
            .finally(() => setAdding(false));
    }

    const handleCancelAdd = () => {
        setAdding(false);
    }

    const handleDelete = (playerStatId) => {
        setDeleting(true);
        DeletePlayerStat(playerStatId)
            .finally(() => setDeleting(false))
    }

    useEffect(() => {
        loadStats();
    }, []);

    useEffect(() => {
        if (match.teamHome) {
            setTeam(match.teamHome)
        }
    }, [match.teamHome]);

    useEffect(() => {
        if (team.id) {
            setAdding(true)
        }
    }, [team]);

    useEffect(() => {
        if (adding === false) {
            loadStats();
        }
    }, [adding]);

    useEffect(() => {
        if (deleting === false) {
            loadStats();
        }
    }, [deleting]);

    const renderStatuses = () => {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Drużyna</th>
                        <th>Zdarzenie</th>
                        <th>Zawodnik</th>
                        <th>Minuta</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {matchStats.map(matchStat => {
                        return (
                            <tr key={matchStat.id}>
                                <td>{matchStat.team.name}</td>
                                <td>{matchStat.statType.statName}</td>
                                <td>{matchStat.player.firstName + " " + matchStat.player.lastName}</td>
                                <td>{matchStat.startMinute}</td>
                                <td>
                                    <button className='btn-primary' onClick={() => handleDelete(matchStat.id)}>Usuń</button>
                                    <button className='btn-invisible' disabled={true}>Anuluj</button>
                                </td>
                            </tr>)
                    }
                    )}
                    {!adding ?
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>
                                <button className='btn-primary' onClick={handleAdd}>Dodaj</button>
                                <button className='btn-invisible' disabled={true}>Anuluj</button>
                            </td>
                        </tr>

                        :
                        <tr>
                            <td><TeamPicker teams={[match.teamHome, match.teamAway]} setCurrentTeam={setTeam} team={team} /></td>
                            <td><StatTypePicker statTypes={statTypes} setStatType={setStatType} statType={statType} /></td>
                            <td><PlayerPicker player={player} setPlayer={setPlayer} players={players} team={team} /></td>
                            <td>
                                <input
                                    name="minute"
                                    type="number"
                                    value={minute > 0 ? minute : ""}
                                    onChange={(event) => setMinute(event.target.value)}
                                />
                            </td>
                            <td>
                                <button className='btn-primary' onClick={handleCancelAdd}>Anuluj</button>
                                <button className='btn-primary' onClick={handlePost}>Dodaj</button>
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
                {renderStatuses()}
                <button className='btn-primary-auto' disabled={match.isFinished}>Zakończ mecz</button>
                <button className='btn-primary-auto' disabled={match.isFinished}>Dodaj zawodników</button>
            </div>
    );
}

export default MatchStats;