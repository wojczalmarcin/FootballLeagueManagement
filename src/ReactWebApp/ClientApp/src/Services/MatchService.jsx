import { HandleResponseError, HandleResponseErrorGet } from "./ResponseErrorHandling";

export const LoadMatchesBySeasonId = (setMatches, currentSeasonId) => {
    return (
        fetch(`Api/Match?seasonId=${currentSeasonId}`)
            .then(HandleResponseErrorGet)
            .then((data) => setMatches(data))
            .catch((error) => {
                alert(error);
                console.error('Error:', error);
            })
    );
}

export const LoadMatchById = (setMatch, matchId) => {
    return (
        fetch(`Api/Match/${matchId}`)
            .then(HandleResponseErrorGet)
            .then((data) => setMatch(data))
            .catch((error) => {
                alert(error);
                console.error('Error:', error);
            })
    );
}

export const LoadLeagueTable = (setLeagueTable, currentSeasonId) => {
    return (
        fetch(`Api/Match/Table/${currentSeasonId}`)
            .then(HandleResponseErrorGet)
            .then((data) => setLeagueTable(data))
            .catch((error) => {
                alert(error);
                console.error('Error:', error);
            })
    );
}

export const DeleteMatch = (matchId) => {
    return (
        fetch(`api/Match/Delete/${matchId}`, {
            method: 'DELETE',
        })
            .then(HandleResponseError)
            .catch(error => {
                alert(error);
                console.error('Error:', error);
            })
    );
}

export const EditMatch = (inputs) => {
    return (
        fetch('api/Match', {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(inputs)
        })
            .then(HandleResponseError)
            .catch(error => {
                alert(error);
                console.error('Error:', error);
            })
    );
}

export const AddMatch = (inputs) => {
    return (
        fetch('api/Match', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(inputs)
        })
            .then(HandleResponseError)
            .catch(error => {
                alert(error);
                console.error('Error:', error);
            })
    );
}
