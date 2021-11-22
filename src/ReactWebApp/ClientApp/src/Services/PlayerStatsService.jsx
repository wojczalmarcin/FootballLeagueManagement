import { HandleResponseError, HandleResponseErrorGet } from "./ResponseErrorHandling";

export const LoadMatchStats = (setMatchStats, matchId) => {
    return (
        fetch(`Api/PlayerStats?matchId=${matchId}`)
            .then(HandleResponseErrorGet)
            .then((data) => setMatchStats(data))
            .catch((error) => {
                alert(error);
                console.error('Error:', error);
            })
    );
}

export const LoadStatTypes = (setStatTypes) => {
    return (
        fetch(`Api/PlayerStats/Type`)
            .then(HandleResponseErrorGet)
            .then((data) => setStatTypes(data))
            .catch((error) => {
                alert(error);
                console.error('Error:', error);
            })
    );
}

export const AddPlayerStat = (inputs) => {
    return (
        fetch('api/PlayerStats', {
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

export const DeletePlayerStat = (playerStatId) => {
    return (
        fetch(`api/PlayerStats/${playerStatId}`, {
            method: 'DELETE',
        })
            .then(HandleResponseError)
            .catch(error => {
                alert(error);
                console.error('Error:', error);
            })
    );
}