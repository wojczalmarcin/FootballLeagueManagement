import { HandleResponseError, HandleResponseErrorGet } from "./ResponseErrorHandling";

export const LoadPlayersByMatchId = (setPlayers, matchId) => {
    return (
        fetch(`Api/Member/Match/${matchId}`)
            .then(response => response.json())
            .then((data) => {
                if (data.responseStatus !== 200) {
                    HandleResponseErrorGet(data);
                }
                else {
                    setPlayers(data.data);
                }
            })
            .catch((error) => {
                console.error('Error:', error);
            })
    );
}