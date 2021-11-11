import { HandleResponseError, HandleResponseErrorGet } from "./ResponseErrorHandling";

export const LoadMatchStats = (setMatchStats, matchId) => {
    return (
        fetch(`Api/PlayerStats?matchId=${matchId}`)
            .then(response => response.json())
            .then((data) => {
                if (data.responseStatus !== 200) {
                    HandleResponseErrorGet(data);
                }
                else {
                    setMatchStats(data.data);
                }
            })
            .catch((error) => {
                console.error('Error:', error);
            })
    );
}

export const LoadStatTypes = (setStatTypes) => {
    return (
        fetch(`Api/PlayerStats/Type`)
            .then(response => response.json())
            .then((data) => {
                if (data.responseStatus !== 200) {
                    HandleResponseErrorGet(data);
                }
                else {
                    setStatTypes(data.data);
                }
            })
            .catch((error) => {
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
            .then(response => response.json())
            .then(data => {
                if (data.responseStatus !== 200) {
                    HandleResponseError(data);
                }
            })
            .catch(error => {
                console.error('Error:', error);
            })
    );
}

export const DeletePlayerStat = (playerStatId) => {
    return (
        fetch(`api/PlayerStats/${playerStatId}`, {
            method: 'DELETE',
        })
            .then(response => response.json())
            .then(data => {
                if (data.responseStatus !== 200) {
                    HandleResponseError(data);
                }
            })
            .catch(error => {
                console.error('Error:', error);
            })
    );
}