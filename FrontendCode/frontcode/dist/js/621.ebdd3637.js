"use strict";(self["webpackChunkfrontcode"]=self["webpackChunkfrontcode"]||[]).push([[621],{5764:function(e,l,t){var a=t(6252),n=(0,a.aZ)({name:"HomeFilled"});const s={xmlns:"http://www.w3.org/2000/svg",viewBox:"0 0 1024 1024"},i=(0,a.Wm)("path",{fill:"currentColor",d:"M512 128 128 447.936V896h255.936V640H640v256h255.936V447.936z"},null,-1);function r(e,l,t,n,r,o){return(0,a.wg)(),(0,a.j4)("svg",s,[i])}n.render=r,n.__file="packages/components/HomeFilled.vue",l["Z"]=n},6621:function(e,l,t){t.r(l),t.d(l,{default:function(){return O}});var a=t(6252);const n={class:"EssayInfo"};function s(e,l,t,s,i,r){const o=(0,a.up)("EssayInfo");return(0,a.wg)(),(0,a.iD)("div",n,[(0,a.Wm)(o)])}var i=t(3577);const r=e=>((0,a.dD)("data-v-fbe15c6a"),e=e(),(0,a.Cn)(),e),o={class:"common-layout"},u={style:{"text-align":"right"}},d={class:"title"},c=r((()=>(0,a._)("br",null,null,-1))),m=(0,a.Uk)(" 摘要 "),p=r((()=>(0,a._)("br",null,null,-1)));function w(e,l,t,n,s,r){const w=(0,a.up)("AsidePart1"),f=(0,a.up)("ElemeFilled"),h=(0,a.up)("Eleme"),v=(0,a.up)("el-icon"),g=(0,a.up)("StarFilled"),_=(0,a.up)("Star"),y=(0,a.up)("HomeFilled"),W=(0,a.up)("el-header"),k=(0,a.up)("el-collapse-item"),I=(0,a.up)("el-table-column"),b=(0,a.up)("el-table"),C=(0,a.up)("el-collapse"),z=(0,a.up)("el-container");return(0,a.wg)(),(0,a.iD)("div",o,[(0,a.Wm)(z,null,{default:(0,a.w5)((()=>[(0,a.Wm)(w),(0,a.Wm)(z,null,{default:(0,a.w5)((()=>[(0,a.Wm)(W,null,{default:(0,a.w5)((()=>[(0,a._)("p",u,[(0,a.Wm)(v,{class:"elementRight",onClick:r.Like},{default:(0,a.w5)((()=>[s.liked?((0,a.wg)(),(0,a.j4)(f,{key:0})):((0,a.wg)(),(0,a.j4)(h,{key:1}))])),_:1},8,["onClick"]),(0,a.Wm)(v,{class:"elementRight",onClick:r.Collect},{default:(0,a.w5)((()=>[!0===s.collected?((0,a.wg)(),(0,a.j4)(g,{key:0})):((0,a.wg)(),(0,a.j4)(_,{key:1}))])),_:1},8,["onClick"]),(0,a.Wm)(v,{class:"elementRight",onClick:r.RouteToHome},{default:(0,a.w5)((()=>[(0,a.Wm)(y)])),_:1},8,["onClick"])])])),_:1}),(0,a._)("div",null,[(0,a._)("p",d,(0,i.zw)(r.essay.title),1)]),(0,a._)("p",null," 作者： "+(0,i.zw)(r.essay.Authors),1),c,m,p,(0,a._)("div",null,(0,i.zw)(r.essay.PaperAbstract),1),(0,a.Wm)(C,null,{default:(0,a.w5)((()=>[(0,a.Wm)(k,{title:"DOI",name:"3"},{default:(0,a.w5)((()=>[(0,a._)("div",null,(0,i.zw)(r.essay.Doi),1)])),_:1}),(0,a.Wm)(k,{title:"发表日期",name:"4"},{default:(0,a.w5)((()=>[(0,a._)("div",null,(0,i.zw)(r.essay.PaperDate),1)])),_:1}),(0,a.Wm)(k,{title:"pdf链接",name:"5"},{default:(0,a.w5)((()=>[(0,a._)("div",null,(0,i.zw)(r.essay.PdfHref),1)])),_:1}),(0,a.Wm)(k,{title:"来源会议",name:"6"},{default:(0,a.w5)((()=>[(0,a._)("div",null,(0,i.zw)(r.essay.Meeting),1)])),_:1}),(0,a.Wm)(k,{title:"bibtex",name:"7"},{default:(0,a.w5)((()=>[(0,a._)("div",null,(0,i.zw)(r.essay.Bibtex),1)])),_:1}),(0,a.Wm)(k,{title:"github",name:"8",onClick:r.getGithubInfo},{default:(0,a.w5)((()=>[(0,a.Wm)(b,{data:s.githubDisplayed},{default:(0,a.w5)((()=>[(0,a.Wm)(I,{prop:"name",label:"github仓库名"}),(0,a.Wm)(I,{prop:"url"})])),_:1},8,["data"])])),_:1},8,["onClick"]),(0,a.Wm)(k,{title:"引用论文",name:"9",onClick:r.getRevelanceInfo},{default:(0,a.w5)((()=>[(0,a.Wm)(b,{data:s.revelanceDisplayed},{default:(0,a.w5)((()=>[(0,a.Wm)(I,{prop:"title",label:"题目"}),(0,a.Wm)(I,{prop:"authors",label:"引用作者"}),(0,a.Wm)(I,{prop:"paperHref",label:"引用论文url"})])),_:1},8,["data"]),(0,a.Wm)(b,{data:s.citationDisplayed},{default:(0,a.w5)((()=>[(0,a.Wm)(I,{prop:"title",label:"题目"}),(0,a.Wm)(I,{prop:"authors",label:"被引用作者"}),(0,a.Wm)(I,{prop:"paperHref",label:"被引用论文url"})])),_:1},8,["data"])])),_:1},8,["onClick"]),(0,a.Wm)(k,{title:"视频链接",name:"10"},{default:(0,a.w5)((()=>[(0,a._)("div",null,(0,i.zw)(r.essay.VideoHref),1)])),_:1}),(0,a.Wm)(k,{title:"arxiv链接",name:"11"},{default:(0,a.w5)((()=>[(0,a._)("div",null,(0,i.zw)(r.essay.ArxivHref),1)])),_:1})])),_:1})])),_:1})])),_:1})])}var f=t(5764),h=(0,a.aZ)({name:"Eleme"});const v={xmlns:"http://www.w3.org/2000/svg",viewBox:"0 0 1024 1024"},g=(0,a.Wm)("path",{fill:"currentColor",d:"M300.032 188.8c174.72-113.28 408-63.36 522.24 109.44 5.76 10.56 11.52 20.16 17.28 30.72v.96a22.4 22.4 0 0 1-7.68 26.88l-352.32 228.48c-9.6 6.72-22.08 3.84-28.8-5.76l-18.24-27.84a54.336 54.336 0 0 1 16.32-74.88l225.6-146.88c9.6-6.72 12.48-19.2 5.76-28.8-.96-1.92-1.92-3.84-3.84-4.8a267.84 267.84 0 0 0-315.84-17.28c-123.84 81.6-159.36 247.68-78.72 371.52a268.096 268.096 0 0 0 370.56 78.72 54.336 54.336 0 0 1 74.88 16.32l17.28 26.88c5.76 9.6 3.84 21.12-4.8 27.84-8.64 7.68-18.24 14.4-28.8 21.12a377.92 377.92 0 0 1-522.24-110.4c-113.28-174.72-63.36-408 111.36-522.24zm526.08 305.28a22.336 22.336 0 0 1 28.8 5.76l23.04 35.52a63.232 63.232 0 0 1-18.24 87.36l-35.52 23.04c-9.6 6.72-22.08 3.84-28.8-5.76l-46.08-71.04c-6.72-9.6-3.84-22.08 5.76-28.8l71.04-46.08z"},null,-1);function _(e,l,t,n,s,i){return(0,a.wg)(),(0,a.j4)("svg",v,[g])}h.render=_,h.__file="packages/components/Eleme.vue";var y=h,W=(0,a.aZ)({name:"ElemeFilled"});const k={xmlns:"http://www.w3.org/2000/svg",viewBox:"0 0 1024 1024"},I=(0,a.Wm)("path",{fill:"currentColor",d:"M176 64h672c61.824 0 112 50.176 112 112v672a112 112 0 0 1-112 112H176A112 112 0 0 1 64 848V176c0-61.824 50.176-112 112-112zm150.528 173.568c-152.896 99.968-196.544 304.064-97.408 456.96a330.688 330.688 0 0 0 456.96 96.64c9.216-5.888 17.6-11.776 25.152-18.56a18.24 18.24 0 0 0 4.224-24.32L700.352 724.8a47.552 47.552 0 0 0-65.536-14.272A234.56 234.56 0 0 1 310.592 641.6C240 533.248 271.104 387.968 379.456 316.48a234.304 234.304 0 0 1 276.352 15.168c1.664.832 2.56 2.56 3.392 4.224 5.888 8.384 3.328 19.328-5.12 25.216L456.832 489.6a47.552 47.552 0 0 0-14.336 65.472l16 24.384c5.888 8.384 16.768 10.88 25.216 5.056l308.224-199.936a19.584 19.584 0 0 0 6.72-23.488v-.896c-4.992-9.216-10.048-17.6-15.104-26.88-99.968-151.168-304.064-194.88-456.96-95.744zM786.88 504.704l-62.208 40.32c-8.32 5.888-10.88 16.768-4.992 25.216L760 632.32c5.888 8.448 16.768 11.008 25.152 5.12l31.104-20.16a55.36 55.36 0 0 0 16-76.48l-20.224-31.04a19.52 19.52 0 0 0-25.152-5.12z"},null,-1);function b(e,l,t,n,s,i){return(0,a.wg)(),(0,a.j4)("svg",k,[I])}W.render=b,W.__file="packages/components/ElemeFilled.vue";var C=W,z=(0,a.aZ)({name:"Star"});const x={xmlns:"http://www.w3.org/2000/svg",viewBox:"0 0 1024 1024"},L=(0,a.Wm)("path",{fill:"currentColor",d:"m512 747.84 228.16 119.936a6.4 6.4 0 0 0 9.28-6.72l-43.52-254.08 184.512-179.904a6.4 6.4 0 0 0-3.52-10.88l-255.104-37.12L517.76 147.904a6.4 6.4 0 0 0-11.52 0L392.192 379.072l-255.104 37.12a6.4 6.4 0 0 0-3.52 10.88L318.08 606.976l-43.584 254.08a6.4 6.4 0 0 0 9.28 6.72L512 747.84zM313.6 924.48a70.4 70.4 0 0 1-102.144-74.24l37.888-220.928L88.96 472.96A70.4 70.4 0 0 1 128 352.896l221.76-32.256 99.2-200.96a70.4 70.4 0 0 1 126.208 0l99.2 200.96 221.824 32.256a70.4 70.4 0 0 1 39.04 120.064L774.72 629.376l37.888 220.928a70.4 70.4 0 0 1-102.144 74.24L512 820.096l-198.4 104.32z"},null,-1);function D(e,l,t,n,s,i){return(0,a.wg)(),(0,a.j4)("svg",x,[L])}z.render=D,z.__file="packages/components/Star.vue";var H=z,E=(0,a.aZ)({name:"StarFilled"});const F={xmlns:"http://www.w3.org/2000/svg",viewBox:"0 0 1024 1024"},Z=(0,a.Wm)("path",{fill:"currentColor",d:"M283.84 867.84 512 747.776l228.16 119.936a6.4 6.4 0 0 0 9.28-6.72l-43.52-254.08 184.512-179.904a6.4 6.4 0 0 0-3.52-10.88l-255.104-37.12L517.76 147.904a6.4 6.4 0 0 0-11.52 0L392.192 379.072l-255.104 37.12a6.4 6.4 0 0 0-3.52 10.88L318.08 606.976l-43.584 254.08a6.4 6.4 0 0 0 9.28 6.72z"},null,-1);function j(e,l,t,n,s,i){return(0,a.wg)(),(0,a.j4)("svg",F,[Z])}E.render=j,E.__file="packages/components/StarFilled.vue";var S=E,A=t(5570),R=t(4239),M=t(2545),V=t(7404),B={name:"EssayInfo",components:{AsidePart1:M.Z,HomeFilled:f.Z,Eleme:y,ElemeFilled:C,Star:H,StarFilled:S},data(){return{githubDisplayed:{},revelanceDisplayed:[],citationDisplayed:[],liked:!1,collected:!1}},mounted(){var e={userId:this.userId,paperId:this.essay.Id};(0,V.uF)(e).then((e=>{200===e.status?this.collected=!0:404===e.status&&(this.collected=!1)})),(0,V.hQ)(e).then((e=>{200===e.status?this.liked=!0:404===e.status&&(this.liked=!1)})),(0,V.Ji)(this.essay.Id).then((e=>{this.githubDisplayed=e.data})),(0,V.getRevelance)(this.essay.Id).then((e=>{this.revelanceDisplayed=e.data.revelance})),(0,V.ux)(this.essay.Id).then((e=>{this.citationDisplayed=e.data.citation}))},methods:{getGithubInfo(){},getRevelanceInfo(){},RouteToHome(){A.Z.push({name:"home"})},Like(){var e={userId:this.userId,paperId:this.essay.Id};!0===this.liked?(0,V.VL)(e):(0,V.dE)(e),this.liked=!this.liked},Collect(){var e={userId:this.userId,paperId:this.essay.Id};!0===this.collected?(0,V.subCollection)(e):(0,V.Hc)(e),this.collected=!this.collected}},computed:{essay(){return R.Z.state.essayS},userId(){return R.Z.state.userId}}},P=t(3744);const G=(0,P.Z)(B,[["render",w],["__scopeId","data-v-fbe15c6a"]]);var T=G,U={name:"UserInfoView",components:{EssayInfo:T}};const J=(0,P.Z)(U,[["render",s]]);var O=J}}]);
//# sourceMappingURL=621.ebdd3637.js.map