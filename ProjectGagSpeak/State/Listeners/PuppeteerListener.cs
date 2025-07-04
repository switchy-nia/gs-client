using GagSpeak.Kinksters;
using GagSpeak.Services.Mediator;
using GagSpeak.State.Managers;

namespace GagSpeak.State.Listeners;

/// <summary> Listeners for components that are not in the toybox compartment nor are visual components. </summary>
/// <remarks> May be catagorized later, but are filtered into here for now. </remarks>
public sealed class PuppeteerListener
{
    private readonly GagspeakMediator _mediator;
    private readonly KinksterManager _pairs;
    private readonly PuppeteerManager _aliasManager;
    public PuppeteerListener(
        GagspeakMediator mediator,
        KinksterManager pairs,
        PuppeteerManager aliasManager)
    {
        _mediator = mediator;
        _pairs = pairs;
        _aliasManager = aliasManager;
    }

    private void PostActionMsg(string enactor, InteractionType type, string message)
    {
        if (_pairs.TryGetNickAliasOrUid(enactor, out var nick))
            _mediator.Publish(new EventMessage(new(nick, enactor, type, message)));
    }

    public void UpdateListener(string pairName, string listenerName)
    {
        _aliasManager.UpdateStoredAliasName(pairName, listenerName);
        PostActionMsg(pairName, InteractionType.ListenerName, $"Updated listener name to {listenerName} for {pairName}.");
    }
}
