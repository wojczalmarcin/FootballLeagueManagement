import { HandleResponseError, HandleResponseErrorGet } from "./ResponseErrorHandling";

export const LoadMatchesBySeasonId = (setMatches, currentSeasonId) => {
    return (
        fetch(`Api/Match?seasonId=${currentSeasonId}`)
            .then(response => response.json())
            .then((data) => {
                if (data.responseStatus !== 200) {
                    HandleResponseErrorGet(data);
                }
                else {
                    setMatches(data.data);
                }
            })
            .catch((error) => {
                console.error('Error:', error);
            })
    );
}

export const LoadMatchById = (setMatch, matchId) => {
    return (
        fetch(`Api/Match/${matchId}`)
            .then(response => response.json())
            .then((data) => {
                if (data.responseStatus !== 200) {
                    HandleResponseErrorGet(data);
                }
                else {
                    setMatch(data.data);
                }
            })
            .catch((error) => {
                console.error('Error:', error);
            })
    );
}

export const DeleteMatch = (matchId) => {
    return (
        fetch(`api/Match/Delete/${matchId}`, {
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

export const EditMatch = (inputs) => {
    console.log(inputs);
    return (
        fetch('api/Match', {
            method: 'PUT',
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

export const AddMatch = (inputs) => {
    return (
        fetch('api/Match', {
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
