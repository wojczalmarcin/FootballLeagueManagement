import React, { useEffect, useState } from 'react';
import { useParams } from "react-router-dom";
import { HandleResponseError } from '../FetchData/ResponseErrorHandling';

const MatchStats = () => {

    const { matchId } = useParams();

    const [loading, setLoading] = useState(true);

    const [matchStats, setMatchStats] = useState([]);

    const loadStats = () => {
        fetch(`Api/Match/Stats/${matchId}`)
            .then(response => response.json())
            .then((data) => {
                if (data.responseStatus !== 200) {
                    HandleResponseError(data);
                }
                else {
                    setMatchStats(data.data);
                }
            })
            .catch((error) => {
                console.log(error);
                alert(error);
            })
            .finally(() => {
                setLoading(false);
            });
    }

    useEffect(() => {
        loadStats();
    }, []);

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
                                    <button className='btn-primary'>Usuń</button>
                                    <button className='btn-primary'>Statystyki</button>
                                </td>
                            </tr>)
                    }
                    )}
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
            </div>
    );
}

export default MatchStats;