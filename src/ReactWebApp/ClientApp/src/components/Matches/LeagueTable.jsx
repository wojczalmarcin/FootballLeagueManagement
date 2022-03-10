import React, { useState, useEffect } from 'react';
import { LoadLeagueTable } from '../../Services/MatchService';
import { LoadSeasonsCurrentSeason } from '../../Services/SeasonService';
import Dropdown from '../SeasonDropdown/Dropdown';

const LeagueTable = () => {

    const [loading, setLoading] = useState(true);

    const [leagueTable, setLeagueTable] = useState([]);

    const [seasons, setSeasons] = useState([]);

    const [currentSeason, setCurrentSeason] = useState({});

    const loadSeasons = () => {
        setLoading(true);
        LoadSeasonsCurrentSeason(setSeasons, setCurrentSeason);
    };

    useEffect(() => {
        if (currentSeason.id) {
           LoadLeagueTable(setLeagueTable,currentSeason.id)
            .then(()=>setLoading(false));
        }
        else
        {
            setLoading(false);
        }
    }, [currentSeason]);

    useEffect(() => {
        loadSeasons();
    }, []);

    const renderLeagueTable = () => {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Drużyna</th>
                        <th>Mecze</th>
                        <th>Punkty</th>
                        <th>Bramki (D)</th>
                        <th>Bramki (W)</th>
                        <th>Bramki stracone (D)</th>
                        <th>Bramki stracone (W)</th>
                        <th>Wygrane (D)</th>
                        <th>Wygrane (W)</th>
                        <th>Przegrane (D)</th>
                        <th>Przegrane (W)</th>
                        <th>Remisy (D)</th>
                        <th>Remisy (W)</th>
                    </tr>
                </thead>
                <tbody>
                    {leagueTable.map(row =>
                        <tr key={row.id}>
                            <td>{row.name}</td>
                            <td>{row.matchesPlayed}</td>
                            <td>{row.points}</td>
                            <td>{row.goalsHome}</td>
                            <td>{row.goalsAway}</td>
                            <td>{row.goalsLostHome}</td>
                            <td>{row.goalsLostAway}</td>
                            <td>{row.wonHome}</td>
                            <td>{row.wonAway}</td>
                            <td>{row.lostHome}</td>
                            <td>{row.lostAway}</td>
                            <td>{row.drawnHome}</td>
                            <td>{row.drawnAway}</td>
                        </tr>
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
                <Dropdown 
                    seasons={seasons}
                    currentSeason={currentSeason} 
                    setCurrentSeason={setCurrentSeason}
                />
                {renderLeagueTable()}
            </div>
    );
}
export default LeagueTable;