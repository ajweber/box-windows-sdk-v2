﻿using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Managers
{
    public class BoxCollaborationWhitelistManager : BoxResourceManager
    {
        public BoxCollaborationWhitelistManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Used to whitelist a domain for a Box collaborator. You can specify the domain and direction of the whitelist. When whitelisted successfully, only users from the whitelisted
        /// domain can be invited as a collaborator. 
        /// </summary>
        /// <param name="domainToWhitelist">This is the domain to whitelist collaboration.</param>
        /// <param name="directionForWhitelist">Can be set to inbound, outbound, or both for the direction of the whitelist.</param>
        /// <returns>The whitelist entry if successfully created.</returns>
        public async Task<BoxCollaborationWhitelistEntry> AddCollaborationWhitelistEntryAsync(String domainToWhitelist, String directionForWhitelist)
        {
            domainToWhitelist.ThrowIfNull("domainToWhitelist");
            directionForWhitelist.ThrowIfNull("directionForWhitelist");

            dynamic req = new JObject();
            req.domain = domainToWhitelist;
            req.direction = directionForWhitelist;

            string jsonStr = req.ToString();

            BoxRequest request = new BoxRequest(_config.CollaborationWhitelistEntryUri)
                .Method(RequestMethod.Post)
                .Payload(jsonStr);

            IBoxResponse<BoxCollaborationWhitelistEntry> response = await ToResponseAsync<BoxCollaborationWhitelistEntry>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to get information about a single collaboration whitelist.
        /// </summary>
        /// <param name="id">Id of the collaboration whitelist object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The collaboration whitelist object is returned. Errors may occur if id is invalid.</returns>
        public async Task<BoxCollaborationWhitelistEntry> GetCollaborationWhitelistEntryAsync(string id, IEnumerable<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.CollaborationWhitelistEntryUri, id)
                .Param(ParamFields, fields);

            IBoxResponse<BoxCollaborationWhitelistEntry> response = await ToResponseAsync<BoxCollaborationWhitelistEntry>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to get information about all collaboration whitelists.
        /// </summary>
        /// <param name="marker">Position to return results from.</param>
        /// <param name="limit">Maximum number of entries to return. Default is 100.</param>
        /// <returns>The collection of collaboration whitelist object is returned.</returns>
        public async Task<BoxCollaborationWhitelistEntryCollection<BoxCollaborationWhitelistEntry>> GetCollaborationWhitelistEntriesAsync(string marker = null, int limit = 100)
        {
            BoxRequest request = new BoxRequest(_config.CollaborationWhitelistEntryUri)
                .Method(RequestMethod.Get)
                .Param("limit", limit.ToString())
                .Param("marker", marker);

            IBoxResponse<BoxCollaborationWhitelistEntryCollection<BoxCollaborationWhitelistEntry>> response =
                await ToResponseAsync<BoxCollaborationWhitelistEntryCollection<BoxCollaborationWhitelistEntry>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to delete a collaboration whitelists.
        /// </summary>
        /// <param name="id">The id of the collaboration whitelist to delete.</param>
        /// <returns>A boolean value indicating whether or not the collaboration whitelist was successfully deleted.</returns>
        public async Task<bool> DeleteCollaborationWhitelistEntryAsync(string id)
        {
            id.ThrowIfNull("id");

            BoxRequest request = new BoxRequest(_config.CollaborationWhitelistEntryUri, id)
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxCollaborationWhitelistEntry> response = await ToResponseAsync<BoxCollaborationWhitelistEntry>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Used to add a user to the exempt user list
        /// </summary>
        /// <param name="user">This is the Box User to add to the exempt list.</param>
        /// <returns>The specific exempt user.</returns>
     /*   public async Task<BoxCollaborationWhitelistTargetEntry> AddCollaborationWhitelistForUserAsync(string id)
        {
            id.ThrowIfNull("id");

            var boxUser = new BoxUser
            {
                Id = id,
                Type = "user"
            };

            BoxRequest request = new BoxRequest(_config.CollaborationWhitelistTargetEntryUri)
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize(boxUser));

            IBoxResponse<BoxCollaborationWhitelistTargetEntry> response = await ToResponseAsync<BoxCollaborationWhitelistTargetEntry>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }*/

        /// <summary>
        /// Used to get information about a single collaboration whitelist for a user.
        /// </summary>
        /// <param name="id">Id of the collaboration whitelist exempt target object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The collaboration whitelist object for a user is returned. Errors may occur if id is invalid.</returns>
        public async Task<BoxCollaborationWhitelistTargetEntry> GetCollaborationWhitelistEntryForUserAsync(string id, IEnumerable<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.CollaborationWhitelistTargetEntryUri, id)
                .Param(ParamFields, fields);

            IBoxResponse<BoxCollaborationWhitelistTargetEntry> response = await ToResponseAsync<BoxCollaborationWhitelistTargetEntry>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to get information about all collaboration whitelists for users.
        /// </summary>
        /// <param name="marker">Position to return results from.</param>
        /// <param name="limit">Maximum number of entries to return. Default is 100.</param>
        /// <returns>The collection of collaboration whitelist object is returned for users.</returns>
        public async Task<BoxCollaborationWhitelistTargetEntryCollection<BoxCollaborationWhitelistTargetEntry>> GetCollaborationWhitelistEntriesForUserAsync(string marker = null, int limit = 100)
        {
            BoxRequest request = new BoxRequest(_config.CollaborationWhitelistTargetEntryUri)
                .Method(RequestMethod.Get)
                .Param("limit", limit.ToString())
                .Param("marker", marker);

            IBoxResponse<BoxCollaborationWhitelistTargetEntryCollection<BoxCollaborationWhitelistTargetEntry>> response =
                await ToResponseAsync<BoxCollaborationWhitelistTargetEntryCollection<BoxCollaborationWhitelistTargetEntry>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to delete a user from the exemption list.
        /// </summary>
        /// <param name="id">The id of the collaboration whitelist to delete for user.</param>
        /// <returns>A boolean value indicating whether or not the collaboration whitelist was successfully deleted for user.</returns>
        public async Task<bool> DeleteCollaborationWhitelistEntryForUserAsync(string id)
        {
            id.ThrowIfNull("id");

            BoxRequest request = new BoxRequest(_config.CollaborationWhitelistEntryUri, id)
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxCollaborationWhitelistTargetEntry> response = await ToResponseAsync<BoxCollaborationWhitelistTargetEntry>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }
    }
}
